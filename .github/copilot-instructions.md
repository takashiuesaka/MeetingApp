# MeetingApp — Copilot Instructions Index

このファイルは各指示ファイルとエージェントの所在マップである。
詳細な指示は以下の各ファイルを参照すること。

## 指示ファイル一覧

| ファイル | 適用対象 | 内容 |
| --- | --- | --- |
| `.github/instructions/design-docs/business-requirements.instructions.md` | リポジトリ全体 | プロダクト方針・機能要件・品質要件 |
| `.github/instructions/product-specs/azure.instructions.md` | リポジトリ全体 | Azure サービス利用方針・認証方針 |
| `.github/instructions/product-specs/audio-capture.instructions.md` | `AudioCapture/`・`Platforms/` | 音声キャプチャ実装方針（F1） |
| `.github/instructions/product-specs/transcription.instructions.md` | `Transcription/`・`Diarization/` | 文字起こし・話者識別実装方針（F2・F5） |
| `.github/instructions/product-specs/translation.instructions.md` | `Translation/` | リアルタイム翻訳・高品質翻訳実装方針（F3・F4） |
| `.github/instructions/product-specs/storage.instructions.md` | `Storage/` | データ保存・エクスポート実装方針（F6） |
| `.github/instructions/design-docs/implementation.instructions.md` | `src/**` | MVVM・サービス分離・非同期処理の共通実装方針、ドキュメント配置規約 |
| `.github/instructions/exec-plans/workflow.instructions.md` | `exec-plans/**` | 実装計画 Markdown の運用規約 |
| `.github/instructions/generated/generated.instructions.md` | `generated/**` | 自動生成物の運用規約 |

## オーケストレーション

| ファイル | 役割 |
| --- | --- |
| `AGENTS.md` | Issue 駆動フロー全体のオーケストレーター。PLAN→検証→実装→テスト→レビューの順序、失敗時の戻し先、exec-plans 運用を定義する。 |

## カスタムエージェント一覧

| ファイル | 役割 |
| --- | --- |
| `agents/plan-agent/AGENTS.md` | PLAN 作成エージェント |
| `agents/plan-verify-agent/AGENTS.md` | PLAN 検証エージェント |
| `agents/implement-agent/AGENTS.md` | 実装エージェント |
| `agents/test-agent/AGENTS.md` | テストエージェント。実装完了後のユニットテスト・統合テスト生成と実行を担当する。 |
| `agents/review-agent/AGENTS.md` | コードレビューエージェント |