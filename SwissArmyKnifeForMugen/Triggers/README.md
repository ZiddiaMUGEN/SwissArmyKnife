# Trigger Breakpoints - Technicals

We offer 2 kinds of trigger breakpoints. Hardware-based uses the debug registers (CPU registers) to force a hardware breakpoint, by analyzing specific memory locations for changes based on the trigger type. Software-based (or 'experimental') uses the mugenWatcher main loop to check when a value has changed and pauses MUGEN when it has.

The main benefit of experimental breakpoints is avoiding potential antivirus flags by not modifying subprocess register values, but they also have the potential to have 1-frame inaccuracies.

Most trigger-related code is in this directory, but the mugenWatcher_DoWork function in MugenWindow.cs is what actually checks the values and confirms if a breakpoint has been met. So if you add a new comparison operator or value type, it needs to go into mugenWatcher_DoWork.

Most other changes go into TriggerDatabase.cs:

- each trigger type gets an entry in the TriggerId enum
- GetTriggerAddrForType function determines the address to monitor for a given TriggerId
- GetTriggerValueType function determines the resulting value for a given TriggerId

New triggers also get added to triggerComboBox in DebugForm.cs and some other stuff in DebugForm.cs (setButton_Click_Handler) for trigger verification.