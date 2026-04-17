# MeetingApp Agent Orchestration

このファイルは、Issue 駆動で Copilot に実装させる際の全体オーケストレーション規約を定義する。

## 基本フロー

1. PLAN 作成
2. PLAN 検証（別モデルを推奨）
3. 実装（段階実行）
4. テスト
5. コードレビュー

上記の順序を必ず守ること。途中失敗時は所定のフェーズへ戻すこと。

仕様と実装ルールの正本は `.github/instructions/` 配下とし、`agents/*.md` を含む補助ドキュメントへの重複記載は最小限にすること。

## フェーズ別担当

- PLAN 作成: `agents/plan-agent.md`
- PLAN 検証: `agents/plan-verify-agent.md`
- 実装: `agents/implement-agent.md`
- テスト: `agents/test-agent.md`
- レビュー: `agents/review-agent.md`

## exec-plans 運用

- 実行中 PLAN は `exec-plans/active/` 配下に配置する。
- 完了 PLAN は `exec-plans/done/` 配下に移動する。
- ファイル名は `YYYYMMDD-issue-<番号>-<短い要約>.md` を推奨する。
- 1 Issue につき PLAN は 1 ファイルを原則とする。

## Issue 仕様ドキュメント化

- Issue に記載された要件は、実装前に仕様 Markdown として明文化すること。
- ビジネス要件は `product-specs/` 配下の Markdown に記載すること。
- 実装要件は `design-docs/` 配下の Markdown に記載すること。
- ビジネス要件と実装要件は必ず別ファイルに分けること。

## Issue ラベル運用

- `phase:plan`
- `phase:plan-verify`
- `phase:implement`
- `phase:test`
- `phase:review`

フェーズ遷移時にはラベルを更新し、現在フェーズを 1 つだけ残すこと。

## 失敗時の戻し先

- PLAN 検証 NG: `phase:plan` に戻す
- 実装 NG: `phase:implement` 継続
- テスト NG: `phase:implement` に戻す
- レビュー NG: `phase:implement` に戻し、修正後 `phase:test` を再実行

## テスト方針の強制

テスト関連の作業は `agents/test-agent.md` を必ず参照し、その規約に従うこと。
`test.prompt.md` は使用しない。
