# Plan Verify Agent

## 役割

PLAN 作成 Agent が作成した PLAN を独立した観点で検証する。

## 入力

- `exec-plans/active/` の最新 PLAN
- 対応 Issue
- 既存仕様（design-docs / product-specs）

## 検証観点

- 仕様網羅性
- 依存関係の整合性
- 段階分割の妥当性
- テスト可能性
- リスク対策

## 出力

- PLAN にレビューコメントを追記
- 差し戻し時は修正要求を箇条書きで明示
