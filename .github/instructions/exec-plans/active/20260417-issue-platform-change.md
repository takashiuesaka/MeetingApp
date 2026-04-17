# PLAN: 実行環境の変更（macOS のみ）

## 目的

対象プラットフォームを Windows + macOS から **macOS のみ** に変更する。

## 対象ファイル

| ファイル | 変更種別 |
| --- | --- |
| `src/MeetingApp/MeetingApp.csproj` | 修正：Windows TFM および Windows 固有プロパティを削除 |
| `.github/instructions/product-specs/business-requirements.instructions.md` | 修正：対象プラットフォームを macOS のみに変更 |
| `.github/instructions/design-docs/audio-capture.instructions.md` | 修正：Windows/WASAPI に関する記述を削除 |
| `.github/instructions/product-specs/20260417-platform-change-business-requirements.md` | 新規作成：ビジネス要件 |

## 実装フェーズ

### Phase 1: ドキュメント整備

- [x] `.github/instructions/product-specs/20260417-platform-change-business-requirements.md` 作成
- [x] `business-requirements.instructions.md` 更新
- [x] `audio-capture.instructions.md` 更新

完了条件: ドキュメントが macOS 専用の内容になっていること。

### Phase 2: プロジェクトファイル修正

- [x] `MeetingApp.csproj` の TargetFrameworks を `net10.0-maccatalyst` のみに変更
- [x] Windows 固有プロパティ（WindowsPackageType, EnableWindowsTargeting, Windows SupportedOSPlatformVersion）を削除

完了条件: csproj に Windows 関連の設定が残っていないこと。

## テスト観点

- macOS ビルドが正常に成功すること
- Windows 関連の記述がコード・ドキュメントに残っていないこと

## リスクと代替案

- Platforms/Windows/ ディレクトリ配下の既存コードは現時点では削除しない（ビルドに含まれないため実害なし）。将来的な削除は別 Issue で対応する。

## 完了条件

- csproj のターゲットが `net10.0-maccatalyst` のみであること
- 仕様・指示ファイルが macOS 専用の記述になっていること
