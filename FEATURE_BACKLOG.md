# CruxSuits Feature Backlog

Use this backlog to capture future features and decide what to develop next. Add ideas as soon as they come up, then refine the items closest to development. Keep one source of truth for status and dependencies. If the team moves tracking to GitHub Issues, copy this template into each feature issue and link the related issues there.

## Workflow

1. **Capture.** Write the user outcome and why it matters. Add a prerequisite if you already know it.
2. **Order.** Review the list weekly or when a prerequisite is completed. Consider user value, urgency, effort, and which work each item unlocks.
3. **Refine.** For the next few items, define a small deliverable and observable acceptance criteria. Link each prerequisite and state what must be working before you can start.
4. **Select.** Pick the highest-value ready item. When a prerequisite is done, check that it provides the behavior you need, then move the dependent item to Ready.
5. **Close.** Mark an item Done after its acceptance criteria are verified and the change is merged. Link the pull request and review any items it unblocks.

Use these statuses: Idea, Blocked, Ready, In progress, Done. A Ready item has clear acceptance criteria and satisfied prerequisites. Use Done to record completed work and preserve links for dependent features.

For the underlying practice of ordering and refining a product backlog, see the [Scrum Guide on the Product Backlog](https://scrumguides.org/scrum-guide.html#product-backlog).

## Feature index

Keep the highest-priority ready items near the top. The address locality picker is an example.

| Feature | Status | Depends on | Next action |
| --- | --- | --- | --- |
| [Coordinate geographic record deletion across services](#coordinate-geographic-record-deletion-across-services) | Idea | Identify every service that stores these IDs and agree how each service updates its references | Inventory consumers and define their cleanup rules |
| Address locality picker | Blocked | A merged locality lookup API that can filter by country | Verify the API contract and link its PR |

## Coordinate geographic record deletion across services

- **Status** — Idea
- **User outcome** — A user can recreate a country, administrative area, or locality with a previously used ID once deletion of the old record is complete.
- **Depends on** — Identify the services that persist geographic IDs and agree how each one replaces or clears references, including when historical records must keep their original meaning.

### Acceptance criteria

- [ ] Soft-deleting a country, administrative area, or locality hides it from active lookups and prevents new references to it.
- [ ] ReferenceDatas durably records a deletion notification for each affected record, including any child records soft-deleted by a cascade. Each service that stores the affected IDs applies its agreed update and confirms completion only after saving it.
- [ ] If a required confirmation is missing or a service fails, the record stays soft-deleted and its ID remains reserved. Notifications can be retried safely, and duplicate or delayed notifications cannot modify references to a recreated record.
- [ ] ReferenceDatas permanently deletes the old record only after every required service has confirmed its update. A user can then create a new record with the same ID, and old references do not resolve to the new record.

## Feature template

Copy this block for each feature. Fill in the outcome first and complete the other fields as the item approaches development.

```markdown
### Feature name

- **Status** — Idea
- **User outcome** —
- **Why it matters** —
- **Depends on** — Link the prerequisite and state the condition that unblocks this feature. Write “None” if there is no dependency.
- **First deliverable** —
- **Owner or issue link** —

#### Acceptance criteria

- [ ] Describe an observable result.
- [ ] Describe the expected behavior for an important edge case.

#### Delivery evidence

- Pull request and verification —
```

## Example feature

### Address locality picker

- **Status** — Blocked
- **User outcome** — A user entering an address can select a locality within the chosen country.
- **Why it matters** — Saved addresses use consistent locality records and are easier to validate.
- **Depends on** — The ReferenceDatas locality lookup is merged and verified to return results scoped to a country.
- **First deliverable** — Add locality search and selection to the address form.

#### Acceptance criteria

- [ ] Searching after selecting a country shows only localities in that country.
- [ ] Changing the country clears any previously selected locality.
- [ ] A search with no matches shows an empty state.
