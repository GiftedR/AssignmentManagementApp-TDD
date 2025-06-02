# Change Plan

**Name:** Colton

**Date:** 2025-05-28

## Purpose
**Bug 2025-341:** Notes not being stored.
**Bug 2025-342:** Notes not being displayed.
**Bug 2025-343:** IsOverdue not working properly.
**Bug 2025-344:** Missing Log entries.

## Changes
**Project**
- Made Priority and DueDate option parameters.
- Made notes in constructor assign to Notes property.
- Added Validation for title and description in constructor and update.
- Removed duplicate implemented methods (FindByTitle and FindAssignmentByTitle) to only use one.
- Implemented DeleteAssignment in ConsoleUI.
- Fixed ToString to show notes.
- Change IsOverdue to test for null DueDate and IsCompleted.
- Added logging for marking assignment complete.
- Added Singletons AssignmentFormatter and ConsoleAppLogger for bug 2025-344.

**Testing**
- Fixed using statements so they didn't throw exceptions.
- Added Test Constructor_WithNotes_ShouldStoreNotes for bug 2025-341.
- Added Test ToString_ShouldShowNotesAsPartOfOutput for bug 2025-342.
- Added Test IsOverdue_WithNoDueDate_ShouldReturnFalse for bug 2025-343.
- Added Test IsOverdue_WithPastDueDate_ShouldReturnTrue for bug 2025-343.
- Added Test IsOverdue_WithIsComplete_ShouldReturnFalse for bug 2025-343.

## Process
- Started by fixing build errors.
- Then started making existing test pass.
- Added tests for bugs needing to be fixed.
- Made failing tests pass.

## Coverage
- Added logging for mark completed.

## Challenges
- Got stuck with API tests, fixed by adding notes to constructor.

## Recommendations
- Logging and showing to the user with every interaction may be too much effort.
- Suggest only showing pertinant information to the user while keeping a file log for bug fixing needs.