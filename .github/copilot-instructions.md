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
| `.github/agents/plan-agent.md` | `plan-agent`。Issue と既存仕様を読み、実装可能な PLAN を作成する。 |
| `.github/agents/plan-verify-agent.md` | `plan-verify-agent`。PLAN 作成 Agent が作成した PLAN を独立した観点で検証する。 |
| `.github/agents/implement-agent.md` | `implement-agent`。承認済み PLAN に従って段階的に実装する。 |
| `.github/agents/test-agent.md` | `test-agent`。PLAN と実装差分を基に、ユニットテスト・統合テストの実装と実行を担当する。 |
| `.github/agents/review-agent.md` | `review-agent`。実装結果が仕様・コード規約・保守性を満たすかを検証する。 |