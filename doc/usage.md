# Usage

This document describes how to run the software.

Requirements:

* Docker

Recommended when developing

* Dotnet SDK
* Powershell

Usage (from project root)

This command will initialize the project environment.

```bash
pwsh Start.ps1
```

Following endpoints are available for user after startup

* Management UI - http://localhost:3000/
* Backend API documentation - http://localhost:5000/swagger/index.html
* SignalR - http://localhost:5000/competition-hub

## Automatic tests

Execute `Build.ps1`

```bash
pwsh Build.ps1
```

## Simulator

Simulator can be used to test how the integrations work when competition is
running. By default, simulator waits 5 seconds between each action to simulate
each wait times (performances, other maintenance pauses, etc). In normal
compettition, the would obviosly be a lot longer.

To start the simulator, execute `Simulator.ps1`
```
pwsh Simulator.ps1
```

Simulator logs are printed to console-container. Simulator data is stored in
`ConsoleClient.Util.Data`-class and the actual execution is done by
`ConsoleClient.SimulatorWorker`.
