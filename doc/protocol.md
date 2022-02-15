# Protocol

NOTE: This is a draft.

This document describes the planned protocol for the services offered by
competition status server.

## SignalR endpoints

[SignalR](https://dotnet.microsoft.com/apps/aspnet/signalr) is used to
communicate state changes to listeners in real time.

**Hub(s)**

* `/competition-hub` Offers competitions status updates, it has following
endpoints
  * **StreamCompetitors** This sends updates when competitor changes.
  Specification is in [doc](generated/openapi-doc.md##schemacurrentcompetitorenvelopemodel)
  * **StreamPerformanceResults** This sends updates when performance results
  are received. Specification is in [doc](generated/openapi-doc.md#schemaperformanceresultsenvelopemodel)

All messages are sent in json format with a metadata part and inner message
with the actual information.

Fields:
* **version** integer value describing protocol version. Can be used for adding
new features, currently probably not needed.
* **type** text value describing type of the content
* **content** json object, different for each message types.
