---
name: feature-completion-review
description: Review a completed Hospital Management roadmap feature for acceptance, verification, documentation, safety, and reusable workflow gaps. Use after a feature is implemented and before declaring it complete. Recommend skill creation or updates only when repeated, stable, non-obvious guidance justifies one; never modify skills without explicit user approval.
---

# Feature Completion Review

Review completed feature work against the repository instructions and active roadmap item. This is a review workflow, not authorization to expand the feature or perform unrelated cleanup.

## Required context

Read `AGENTS.md`, `ROADMAP.md`, the relevant implementation, and the current diff. Identify the feature's stated or reasonably implied acceptance criteria before evaluating completion.

## Review

Evaluate the completed feature across these areas:

1. **Behavior**
   - Confirm the implementation covers the complete user workflow rather than only isolated UI or persistence changes.
   - Check important success, validation, empty, failure, cancellation, and permission paths that apply to the feature.

2. **Architecture and data**
   - Preserve the repository's WPF, MVVM, dependency-injection, data-manager, and EF Core boundaries.
   - Check entity relationships, nullability, concurrency, auditability, and migration implications where applicable.
   - Flag destructive or irreversible data behavior explicitly.

3. **Healthcare safety and privacy**
   - Look for unintended sensitive-data exposure, missing authorization, inadequate audit history, unsafe deletion, or misleading clinical state.
   - Do not claim regulatory compliance or provide legal certification.

4. **Verification**
   - Confirm the full solution builds.
   - Confirm relevant automated tests exist and pass. If test infrastructure is missing, identify the smallest useful missing test rather than treating manual verification as sufficient indefinitely.
   - For UI changes, verify bindings, commands, validation, navigation, error presentation, and modal behavior.
   - For persistence changes, verify migrations and CRUD behavior against a disposable or explicitly approved database.

5. **Documentation and roadmap**
   - Confirm user-visible behavior, setup changes, and operational requirements are documented where needed.
   - Recommend marking a roadmap item complete only when its acceptance criteria and verification evidence are satisfied.

## Skill-gap decision

Recommend **no skill change** when the issue is a one-time detail, already covered, generic engineering knowledge, or not demonstrated by real work.

Recommend **updating an existing skill** when its workflow already covers the situation and a demonstrated reusable omission materially affected the result.

Recommend **creating a new skill** only when the guidance is likely to recur, non-obvious, stable, distinct from existing instructions, and better represented as a workflow than as code, tests, or documentation.

For a proposed skill change, report:

- Proposed skill name.
- Whether to create or update.
- Triggering requests and explicit exclusions.
- The demonstrated problem it addresses.
- Essential reusable instructions or resources.
- Why `AGENTS.md`, tests, or code are insufficient instead.

Do not create, edit, install, or delete a skill during this review. Ask for explicit user approval first; after approval, use the `skill-creator` skill for the change.

## Output

Report:

1. Completion verdict: **complete**, **complete with follow-up**, or **incomplete**.
2. Acceptance criteria and evidence.
3. Defects, risks, or missing verification, ordered by severity.
4. Documentation and roadmap updates needed.
5. Skill-gap decision: **none**, **update existing**, or **propose new**, with rationale.
6. The single next action.

Do not make implementation changes unless the user separately requests fixes.
