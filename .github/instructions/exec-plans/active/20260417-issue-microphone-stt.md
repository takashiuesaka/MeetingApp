# PLAN: マイク入力リアルタイム音声認識（STT）画面表示

## 目的

macOS のマイクデバイスを音声ソースとして Azure Speech Service を使用し、リアルタイムに音声認識（STT）した結果を画面に表示する機能を実装する。

## 対象ファイル

| ファイル | 変更種別 |
| --- | --- |
| `src/MeetingApp/Services/Transcription/IMicrophoneTranscriptionService.cs` | 新規作成：サービスインターフェース定義 |
| `src/MeetingApp/Services/Transcription/MicrophoneTranscriptionService.cs` | 新規作成：Azure Speech SDK を使用した実装 |
| `src/MeetingApp/Services/Transcription/TranscriptionModels.cs` | 新規作成：データモデル（PartialResult, FinalResult, Entry） |
| `src/MeetingApp/ViewModels/MainViewModel.cs` | 修正：STT 開始/停止コマンド、バインディング用プロパティ追加 |
| `src/MeetingApp/Views/MainPage.xaml` | 修正：暫定テキスト表示ラベル、確定テキスト一覧、開始/停止ボタン追加 |
| `src/MeetingApp/Views/MainPage.xaml.cs` | 修正：必要に応じて自動スクロール処理追加 |
| `src/MeetingApp/MauiProgram.cs` | 修正：`IMicrophoneTranscriptionService` を DI コンテナに登録 |
| `src/MeetingApp/MeetingApp.csproj` | 修正：`Microsoft.CognitiveServices.Speech` NuGet パッケージ追加 |
| `src/MeetingApp/Platforms/MacCatalyst/Info.plist` | 修正：`NSMicrophoneUsageDescription` キー追加 |
| `.github/instructions/design-docs/transcription.instructions.md` | 修正：F2-Mic 実装仕様を追記（実施済み） |
| `.github/instructions/product-specs/20260417-issue-microphone-stt-business-requirements.md` | 新規作成：ビジネス要件（実施済み） |
| `.github/instructions/product-specs/business-requirements.instructions.md` | 修正：STT ビジネス要件の要約を追記（実施済み） |

> **判断理由（新規 vs 既存修正）**:
> `transcription.instructions.md` は文字起こし全般を扱うスコープと重複するため既存ファイルへの追記で対応した。
> サービスコード（`IMicrophoneTranscriptionService` 等）は新規ファイル作成とする。

## 実装フェーズ一覧

### Phase 1: 仕様・ドキュメント整備（完了）

- [x] `20260417-issue-microphone-stt-business-requirements.md` 作成
- [x] `business-requirements.instructions.md` 更新（要約・リンク追記）
- [x] `transcription.instructions.md` 更新（F2-Mic 実装仕様追記）

**完了条件**: ビジネス要件 Markdown が存在し、`business-requirements.instructions.md` にリンクが含まれていること。`transcription.instructions.md` に F2-Mic セクションが存在すること。

---

### Phase 2: NuGet パッケージ追加 & プロジェクト設定

- [ ] `MeetingApp.csproj` に `Microsoft.CognitiveServices.Speech` を追加
- [ ] `Platforms/MacCatalyst/Info.plist` に `NSMicrophoneUsageDescription` を追加

**完了条件**: `dotnet restore` が成功し、`Info.plist` にマイクアクセス記述キーが存在すること。

---

### Phase 3: データモデル & サービスインターフェース実装

- [ ] `TranscriptionModels.cs` に `TranscriptionPartialResult`、`TranscriptionFinalResult`、`TranscriptionEntry` を定義
- [ ] `IMicrophoneTranscriptionService.cs` にインターフェースを定義（`StartAsync`、`StopAsync`、`Recognizing` イベント、`Recognized` イベント）

**完了条件**: インターフェースとモデルがビルドエラーなくコンパイルできること。

---

### Phase 4: サービス実装（Azure Speech SDK）

- [ ] `MicrophoneTranscriptionService.cs` を実装
  - `DefaultAzureCredential`（Interactive Browser のみ有効）で Speech 用 AAD トークンを取得し、`SpeechConfig.FromAuthorizationToken` を構成
  - AAD トークンは Start 時に 1 回だけ設定するのではなく、失効前に `Azure.Identity` 経由で再取得し、連続認識中の `SpeechConfig` / recognizer に再設定する更新処理を実装
  - トークン取得・更新・キャッシュは `Azure.Identity` に委譲し、アプリケーション側ではアクセストークン/リフレッシュトークンを永続保存しない
  - `AudioConfig.FromDefaultMicrophoneInput()` でマイク入力を構成
  - `SpeechRecognizer.StartContinuousRecognitionAsync()` で連続認識開始
  - `Recognizing` イベントで `IMicrophoneTranscriptionService.Recognizing` を発火
  - `Recognized` イベントで `IMicrophoneTranscriptionService.Recognized` を発火
- [ ] `MauiProgram.cs` に `IMicrophoneTranscriptionService` → `MicrophoneTranscriptionService` の DI 登録

**完了条件**: サービスがビルドエラーなくコンパイルでき、DI 登録が存在し、長時間の連続認識でも AAD トークン失効前に Azure.Identity 経由で再取得・再設定する方針が実装内容として明記されていること。

---

### Phase 5: ViewModel 実装

- [ ] `MainViewModel.cs` に以下を追加:
  - `string PartialTranscript` プロパティ（`INotifyPropertyChanged`）
  - `ObservableCollection<TranscriptionEntry> TranscriptEntries` プロパティ
  - `bool IsRecording` プロパティ
  - `ICommand StartCommand`（`StartAsync` 呼び出し、`IsRecording = true`）
  - `ICommand StopCommand`（`StopAsync` 呼び出し、`IsRecording = false`）
  - `Recognizing` イベントハンドラ → `PartialTranscript` 更新（メインスレッド）
  - `Recognized` イベントハンドラ → `TranscriptEntries` に追加・`PartialTranscript` クリア（メインスレッド）

**完了条件**: ViewModel がビルドエラーなくコンパイルでき、コマンド・プロパティが MVVM パターンに準拠していること。

---

### Phase 6: UI 実装（View）

- [ ] `MainPage.xaml` に以下を追加:
  - 開始ボタン（`StartCommand` バインド、`IsRecording` が true のとき無効化）
  - 停止ボタン（`StopCommand` バインド、`IsRecording` が false のとき無効化）
  - 録音中インジケーター（`IsRecording` バインド）
  - 暫定テキスト表示ラベル（`PartialTranscript` バインド、イタリック等で区別）
  - 確定テキスト一覧（`CollectionView` + `TranscriptEntries` バインド、タイムスタンプ付き）
- [ ] `MainPage.xaml.cs` に最新エントリへの自動スクロール処理を追加（必要に応じて）

**完了条件**: UI がバインディングエラーなくレンダリングでき、ボタン・ラベル・一覧が正常に表示されること。

---

### Phase 7: 動作確認・テスト

- [ ] macOS 実機または Simulator でマイク権限ダイアログが表示されること
- [ ] 開始ボタン押下で連続認識が開始されること
- [ ] 発話中に暫定テキストがリアルタイムに更新されること
- [ ] 発話完了後に確定テキストが一覧に追加されること
- [ ] 停止ボタン押下で認識が停止し、インジケーターが消えること

**完了条件**: 上記すべての動作が確認できること。

## テスト観点

| 観点 | 内容 |
| --- | --- |
| 正常系: 音声認識開始 | 開始ボタン押下後、マイクから音声を入力すると暫定テキストが表示される |
| 正常系: 確定テキスト蓄積 | 発話ごとに確定テキストが一覧に追加される |
| 正常系: 認識停止 | 停止ボタン押下後、音声を入力しても新たな認識が行われない |
| 異常系: マイク権限拒否 | マイクアクセスが拒否された場合、適切なエラーメッセージを表示する |
| 異常系: Azure 認証失敗 | 認証に失敗した場合、適切なエラーメッセージを表示し録音状態をリセットする |
| 非機能: 遅延 | 発話から暫定テキスト表示までが 2 秒以内であること |
| 非機能: スレッド安全 | UI 更新がメインスレッドで行われること |

## リスクと代替案

| リスク | 影響 | 代替案 |
| --- | --- | --- |
| `Microsoft.CognitiveServices.Speech` の macOS（MacCatalyst）対応バージョン確認が必要 | Phase 2 でビルドが失敗する可能性 | 対応バージョンを公式ドキュメントで確認し、必要に応じてバージョン固定する |
| `DefaultAzureCredential` の `InteractiveBrowserCredential` がサンドボックス環境で動作しない場合 | 認証フローが失敗する | 他の Credential へフォールバックせず、`InteractiveBrowserCredential` が動作する実行形態・ブラウザ起動導線・権限制約を確認して調整する |
| 自動スクロールの MacCatalyst 固有の挙動差異 | 最新エントリが表示されない場合あり | `ScrollView.ScrollToAsync` または `CollectionView.ScrollTo` で対応 |

## 完了条件

- [ ] ビジネス要件 Markdown が作成され `business-requirements.instructions.md` にリンクされていること
- [ ] `transcription.instructions.md` に F2-Mic 実装仕様が追記されていること
- [ ] `IMicrophoneTranscriptionService` インターフェースと `MicrophoneTranscriptionService` 実装が存在すること
- [ ] DI コンテナにサービスが登録されていること
- [ ] `MainViewModel` に STT 用プロパティ・コマンドが追加され MVVM パターンに準拠していること
- [ ] `MainPage.xaml` に開始/停止ボタン・暫定テキスト表示・確定テキスト一覧が存在すること
- [ ] `Info.plist` に `NSMicrophoneUsageDescription` が存在すること
- [ ] macOS 上でマイク入力によるリアルタイム STT と画面表示が動作すること
