# Competition status server

Server which provides a status for ongoing competition. Designed for sport competitions.

Planned features

1. Sending data of currently ongoing performance to listeners when performance
starts.
1. Sending duration of currently ongoing performance, if applicable.
1. Offering data package of the whole competition (current results,
competitors etc) when requested.

Protocol planning is in [protocol.md](doc/protocol.md)

Project structure

* **doc** Documentation
* **src** Source files

## Usage

See [usage documentation](doc/usage.md). This will be updated to describe the
current way to execute the software and test it.

## Automatic tests

Execute `Build.ps1`

```bash
pwsh Build.ps1
```

## Simulator

Simulator can be used to thest how the integrations work when competition is
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

## License

See [LICENSE](LICENSE)
