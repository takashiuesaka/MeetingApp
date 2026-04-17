---
applyTo: "src/**/Services/Transcription/**,src/**/Services/Diarization/**"
---

## 文字起こし実装方針（F2）

- キャプチャした音声をリアルタイムにテキストへ変換すること。
- STT エンジンは Azure Speech Service を使用すること。
- 認証は DefaultAzureCredential（InteractiveBrowserCredential のみ有効）を使用し、API キー認証は使用しないこと。
- 対応言語は英語を主対象とし、多言語対応を視野に入れた設計にすること。
- 書き起こし結果はタイムスタンプ付きで保存すること。

## マイク入力リアルタイムSTT 実装仕様（F2-Mic）

### サービスインターフェース

- `IMicrophoneTranscriptionService` インターフェースを `src/MeetingApp/Services/Transcription/` に定義すること。
- 以下のメンバーを持つこと:
  - `StartAsync(CancellationToken)` — 連続音声認識を開始する非同期メソッド
  - `StopAsync()` — 連続音声認識を停止する非同期メソッド
  - `event EventHandler<TranscriptionPartialResult> Recognizing` — 暫定結果イベント
  - `event EventHandler<TranscriptionFinalResult> Recognized` — 確定結果イベント

### Azure Speech SDK 使用方針

- `Microsoft.CognitiveServices.Speech` NuGet パッケージを使用すること。
- `SpeechConfig` の認証は `DefaultAzureCredential`（`InteractiveBrowserCredential` のみ有効）を使用して取得した AAD トークンを `SpeechConfig.FromAuthorizationToken` で設定すること。
- 音声入力は `AudioConfig.FromDefaultMicrophoneInput()` を使用し、macOS デフォルトマイクデバイスから取得すること。
- `SpeechRecognizer.StartContinuousRecognitionAsync()` を使用して連続認識を行うこと。
- `Recognizing` イベントで暫定テキストを、`Recognized` イベントで確定テキストをそれぞれ発行すること。

### ViewModel / UI 実装方針

- `MainViewModel` に以下のプロパティ・コマンドを追加すること:
  - `string PartialTranscript` — 認識中の暫定テキスト（バインディング用）
  - `ObservableCollection<TranscriptionEntry> TranscriptEntries` — 確定済みテキストの一覧
  - `ICommand StartCommand` — 録音開始コマンド
  - `ICommand StopCommand` — 録音停止コマンド
  - `bool IsRecording` — 録音中フラグ（UI フィードバック用）
- UI 更新は `MainThread.BeginInvokeOnMainThread` を使用して必ずメインスレッドで行うこと。

### データモデル

- `TranscriptionPartialResult` — `string Text` を持つ暫定結果モデル
- `TranscriptionFinalResult` — `string Text`、`DateTimeOffset Timestamp` を持つ確定結果モデル
- `TranscriptionEntry` — UI バインディング用の確定済みエントリ（`string Text`、`DateTimeOffset Timestamp`）

### UI 表示仕様

- 暫定テキストは専用ラベルにイタリック等で区別表示すること。
- 確定テキストはスクロール可能な `CollectionView` または `ScrollView + StackLayout` で一覧表示すること。
- 最新エントリが常に表示されるよう自動スクロールすること。
- 録音中は開始ボタンを無効化し、録音中インジケーター（例: 赤丸アイコンまたはラベル）を表示すること。

### macOS パーミッション

- `Platforms/MacCatalyst/Info.plist` に `NSMicrophoneUsageDescription` キーを追加し、マイクアクセス理由を記述すること。

## 話者識別実装方針（F5）

- 音声ストリーム内の話者を識別し、匿名ラベル（Speaker A, Speaker B, …）を割り当てること。
- 話者識別エンジンは Azure Speech Service の Speaker Diarization 機能を使用すること。
- 同一セッション内で一貫したラベルを維持すること。
- 話者の実名は扱わず、匿名性を保つこと。
