# Protocol

NOTE: This is a draft.

This document describes the planned protocol for the services offered by
competition status server.


## SignalR endpoints

[SignalR](https://dotnet.microsoft.com/apps/aspnet/signalr) is used to
communicate state changes to listeners in real time.

All messages are sent in json format with a metadata part and inner message
with the actual information.

Fields:
* **version** integer value describing protocol version. Can be used for adding
new features, currently probably not needed.
* **type** text value describing type of the content
* **content** json object, different for each message types.

Following subsections describes different message types.

### current-competitor

This type describes who are the current competitors and in what divisions.

Fields:

* **divisions** text value describing currently ongoing divisions that the
competitor
is competing in.
* **attempt** A text value describing attempt number, or otherwise giving
identifying information if there are multiple attempts per competitor. This
field may be null if not used.
* **category** A text value describing which category is performed by
competitor. This field may be null if not used.
* **target** A text value describing the target for the attempt. This
field may be empty if there is no target specified.
* **current-place** A number containg current place for competitor. This field
may be null if competitor doesn't have a placement yet.
* **competitors** an array containing [Competitors](objects.md#Competitor)
currently competing. This array should contain one or more competitors.
* **previous-results** an array containing previous results for for categories.
This array may be empty if not used or if no previous results exists. See
[Single category result](objects.md#Single-category-result) for specification

Example
```json
{
    "version": 1,
    "type": "current-competitor",
    "content": {
        "divisions": "senior women",
        "category": "hauling",
        "target": "120kg",
        "attempt": "3",
        "current-place": 3,
        "competitors": [
            {
                "name": "last name, first name",
                "team": "sport team here",
                "competitor-id": "123"
            },
            {
                "name": "last name, first name",
                "team": "sport team here",
                "competitor-id": "124"
            }
        ],
        "previous-results": [
            {
                "category": "hauling",
                "attempt": "3",
                "result": "See objects.md#VahvinSM and Otevoima result"
            }
        ]
    }
}
```

### performance-results

This is sent when performance has ended and results has been calculated. This
may or may not be for the previous competitor, depending on the way that the
event has been organized.

Fields:

* **divisions** text value describing currently ongoing divisions that the
competitor is competing in.
* **attempt** text value describing attempt number, or otherwise giving
identifying information if there are multiple attempts per competitor. This
field may be null if not used.
* **category** A text value describing which category is performed by
competitor. This field may be null if not used.
* **target** A text value describing the target for the attempt. This
field may be empty if there is no target specified.
* **current-place** A number containg current place for competitor. This field
may be null if competitor doesn't have a placement yet.
* **competitors** an array containing [Competitors](objects.md#Competitor)
currently competing. This array should contain one or more competitors.
* **results** an object containing results. Actual content differs between
different events and should be agreed before event.

Example
```json
{
    "version": 1,
    "type": "performance-results",
    "content": {
        "divisions": "senior women",
        "attempt": "3",
        "category": "hauling",
        "target": "120kg",
        "current-place": 3,
        "competitors": [
            {
                "name": "last name, first name",
                "team": "sport team here",
                "competitor-id": "123"
            },
            {
                "name": "last name, first name",
                "team": "sport team here",
                "competitor-id": "124"
            }
        ],
        "results": {
            "category a": "string",
            "category b": "string",
            "category c": "string",
        }
    }
}
```

Address http://address/hub/

## UDP Time server

UDP time server sends current performances duration when the performance is
ongoing. Datagrams are sent to predefined addresses/port(s) which should be
configured before event.

Datagram should contain only one value, Signed 64-bit integer which is the
duration in milliseconds.

## Competition status server endpoint

This endpoint can be used to get the current status of the whole competition.

TODO: Define

Fields

* **event** Name of the competition. 
* **createdAt** UTC time of generation of these results.

Example:

```json
{
    "event": "name of the competiton.",
    "createdAt": "2021-11-29T17:29:01Z",
    "divisions": [
        {
            "division": "senior women",
            "results": [
                {
                    "competitors": [
                        {
                            "name": "last name, first name",
                            "team": "sport team here",
                        },
                        {
                            "name": "last name, first name",
                            "team": "sport team here",
                        }
                    ],
                    "place": 1,
                    "finalScore": {
                        "category a": "string",
                        "category b": "string",
                        "category c": "string",
                    }
                },
                {
                    "competitors": [
                        {
                            "name": "last name, first name",
                            "team": "sport team here",
                        },
                        {
                            "name": "last name, first name",
                            "team": "sport team here",
                        }
                    ],
                    "place": 2,
                    "finalScore": {
                        "category a": "string",
                        "category b": "string",
                        "category c": "string",
                    }
                }
            ]
        }
    ]
}
```
