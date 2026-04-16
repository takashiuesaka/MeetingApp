# Plan Agent

## 役割

Issue と既存仕様を読み、実装可能な PLAN を作成する。

## 入力

- GitHub Issue 本文
- `.github/copilot-instructions.md`
- `.github/instructions/design-docs/**/*.instructions.md`
- `.github/instructions/product-specs/**/*.instructions.md`

## 出力

- `exec-plans/active/` 配下に PLAN Markdown を作成
- PLAN は段階（Phase）ごとに分割し、各段階で完了条件を明記

## PLAN テンプレート

- 目的
- 対象ファイル
- 実装フェーズ一覧
- テスト観点
- リスクと代替案
- 完了条件
