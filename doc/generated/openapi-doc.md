---
title: Competition Status API v1
language_tabs:
  - http: HTTP
language_clients:
  - http: ""
toc_footers: []
includes: []
search: true
highlight_theme: darkula
headingLevel: 2

---

<!-- Generator: Widdershins v4.0.1 -->

<h1 id="competition-status-api">Competition Status API v1</h1>

> Scroll down for code samples, example requests and responses. Select a language for code samples from the tabs above or the mobile navigation menu.

API which provides status information for sports competition.
                        See https://github.com/Hekku2/competition-status-server/ for more information.

<h1 id="competition-status-api-competition">Competition</h1>

## CompetitionGetCurrentCompetitor

<a id="opIdCompetitionGetCurrentCompetitor"></a>

> Code samples

```http
GET /Competition/current-competitor HTTP/1.1

Accept: text/plain

```

`GET /Competition/current-competitor`

*Current competitor information*

> Example responses

> 200 Response

```
{"version":"string","type":"string","content":{"division":"Senior Women","competitors":[{"name":"Matt Smith","team":"Team Pole Queens"}]}}
```

```json
{
  "version": "string",
  "type": "string",
  "content": {
    "division": "Senior Women",
    "competitors": [
      {
        "name": "Matt Smith",
        "team": "Team Pole Queens"
      }
    ]
  }
}
```

<h3 id="competitiongetcurrentcompetitor-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|Success|[CurrentCompetitorEnvelopeModel](#schemacurrentcompetitorenvelopemodel)|

<aside class="success">
This operation does not require authentication
</aside>

## CompetitionGetResults

<a id="opIdCompetitionGetResults"></a>

> Code samples

```http
GET /Competition/result-history HTTP/1.1

Accept: text/plain

```

`GET /Competition/result-history`

*Returns history of reported results. This is mainly used for debugging purposes
and should not be used for reporting.*

> Example responses

> 200 Response

```
[{"version":"string","type":"string","content":{"division":"Senior Women","currentPlace":1,"competitors":[{"name":"Matt Smith","team":"Team Pole Queens"}],"result":{"total":127.266,"artisticScore":59.266,"executionScore":70.333,"difficultyScore":12.8,"headJudgePenalty":0}}}]
```

```json
[
  {
    "version": "string",
    "type": "string",
    "content": {
      "division": "Senior Women",
      "currentPlace": 1,
      "competitors": [
        {
          "name": "Matt Smith",
          "team": "Team Pole Queens"
        }
      ],
      "result": {
        "total": 127.266,
        "artisticScore": 59.266,
        "executionScore": 70.333,
        "difficultyScore": 12.8,
        "headJudgePenalty": 0
      }
    }
  }
]
```

<h3 id="competitiongetresults-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|Success|Inline|

<h3 id="competitiongetresults-responseschema">Response Schema</h3>

Status Code **200**

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|*anonymous*|[[PerformanceResultsEnvelopeModel](#schemaperformanceresultsenvelopemodel)]|false|none|[Message containing performance results]|
|» version|string|true|read-only|Envelope version number. Version can be discarded if no<br>functionality is specified for given version|
|» type|string|true|read-only|Type of the message. This and version can be used to identify<br>correct parser for this message.|
|» content|[PerformanceResultsContentModel](#schemaperformanceresultscontentmodel)|false|none|Describes a result for competitor(s) in for a single performance and what<br>place it did achieve, if any.|
|»» division|string|true|none|Name of the division. Should match some division in currenlty active<br>competition.|
|»» currentPlace|integer(int32)¦null|false|none|Placement that the competitor(s) received with this result. This is null<br>If current place couldn't be calculated.<br>Starts from 1.|
|»» competitors|[[CompetitorModel](#schemacompetitormodel)]|true|none|Competitors that did the performance. This should contain at least one<br>item.|
|»»» name|string¦null|false|none|Name of competitor|
|»»» team|string¦null|false|none|Team of competitor|
|»» result|[PoleSportResultModel](#schemapolesportresultmodel)|true|none|Represents a score in Pole Dance Sport series|
|»»» total|number(double)|true|none|Calculated total score (A+E+D-HJ).<br>This is used to sort the score board.|
|»»» artisticScore|number(double)|true|none|Artistic score (A)|
|»»» executionScore|number(double)|true|none|Execution score (E)|
|»»» difficultyScore|number(double)|true|none|Difficulty score (D)|
|»»» headJudgePenalty|number(double)|true|none|Head judge penalty (HJ). This is subtracted from the total.|

<aside class="success">
This operation does not require authentication
</aside>

## CompetitionSetCurrentCompetitor

<a id="opIdCompetitionSetCurrentCompetitor"></a>

> Code samples

```http
POST /Competition/set-current-competitor HTTP/1.1

Content-Type: application/json

```

`POST /Competition/set-current-competitor`

*Sets current competitor or remove current competitor.*

> Body parameter

```json
{
  "id": 123
}
```

<h3 id="competitionsetcurrentcompetitor-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|body|body|[CurrentCompetitorSetModel](#schemacurrentcompetitorsetmodel)|false|Competitor ID|

<h3 id="competitionsetcurrentcompetitor-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|Success|None|

<aside class="success">
This operation does not require authentication
</aside>

## CompetitionSetResult

<a id="opIdCompetitionSetResult"></a>

> Code samples

```http
POST /Competition/set-result HTTP/1.1

Content-Type: application/json

```

`POST /Competition/set-result`

*Set result for competitor*

> Body parameter

```json
{
  "id": 2,
  "results": {
    "artisticScore": 59.266,
    "executionScore": 70.333,
    "difficultyScore": 12.8,
    "headJudgePenalty": 0
  }
}
```

<h3 id="competitionsetresult-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|body|body|[CompetitorResultModel](#schemacompetitorresultmodel)|false|none|

<h3 id="competitionsetresult-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|Success|None|

<aside class="success">
This operation does not require authentication
</aside>

## CompetitionUploadCompetition

<a id="opIdCompetitionUploadCompetition"></a>

> Code samples

```http
POST /Competition/upload-competition HTTP/1.1

Content-Type: application/json

```

`POST /Competition/upload-competition`

*Upload compettion data. This overrides all data*

> Body parameter

```json
{
  "name": "National finals 2022",
  "divisions": [
    {
      "name": "Senior Women",
      "items": [
        {
          "id": 123,
          "competitors": [
            {
              "name": "Matt Smith",
              "team": "Team Pole Queens"
            }
          ],
          "forfeit": true,
          "results": {
            "artisticScore": 59.266,
            "executionScore": 70.333,
            "difficultyScore": 12.8,
            "headJudgePenalty": 0
          }
        }
      ]
    }
  ],
  "currentCompetitor": {
    "id": 25,
    "division": "Senior Women",
    "competitors": [
      {
        "name": "Matt Smith",
        "team": "Team Pole Queens"
      }
    ]
  }
}
```

<h3 id="competitionuploadcompetition-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|body|body|[CompetitionFileModel](#schemacompetitionfilemodel)|false|model representing the json file|

<h3 id="competitionuploadcompetition-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|Success|None|

<aside class="success">
This operation does not require authentication
</aside>

## CompetitionGetCompetitionStatus

<a id="opIdCompetitionGetCompetitionStatus"></a>

> Code samples

```http
GET /Competition/competition-status HTTP/1.1

Accept: text/plain

```

`GET /Competition/competition-status`

*Returns current status for the whole competition*

> Example responses

> 200 Response

```
{"version":"string","type":"string","content":{"eventName":"National finals 2022","createdAt":"2022-02-15T19:14:25.004Z","divisions":[{"name":"Senior women","results":[{"id":0,"competitors":[{"name":"Matt Smith","team":"Team Pole Queens"}],"result":{"total":127.266,"artisticScore":59.266,"executionScore":70.333,"difficultyScore":12.8,"headJudgePenalty":0},"forfeit":true}],"forfeited":[{"id":0,"competitors":[{"name":"Matt Smith","team":"Team Pole Queens"}],"result":{"total":127.266,"artisticScore":59.266,"executionScore":70.333,"difficultyScore":12.8,"headJudgePenalty":0},"forfeit":true}],"upcomingCompetitorModels":[{"id":123,"competitors":[{"name":"Matt Smith","team":"Team Pole Queens"}]}]}],"currentCompetitor":{"division":"Senior Women","competitors":[{"name":"Matt Smith","team":"Team Pole Queens"}]}}}
```

```json
{
  "version": "string",
  "type": "string",
  "content": {
    "eventName": "National finals 2022",
    "createdAt": "2022-02-15T19:14:25.004Z",
    "divisions": [
      {
        "name": "Senior women",
        "results": [
          {
            "id": 0,
            "competitors": [
              {
                "name": "Matt Smith",
                "team": "Team Pole Queens"
              }
            ],
            "result": {
              "total": 127.266,
              "artisticScore": 59.266,
              "executionScore": 70.333,
              "difficultyScore": 12.8,
              "headJudgePenalty": 0
            },
            "forfeit": true
          }
        ],
        "forfeited": [
          {
            "id": 0,
            "competitors": [
              {
                "name": "Matt Smith",
                "team": "Team Pole Queens"
              }
            ],
            "result": {
              "total": 127.266,
              "artisticScore": 59.266,
              "executionScore": 70.333,
              "difficultyScore": 12.8,
              "headJudgePenalty": 0
            },
            "forfeit": true
          }
        ],
        "upcomingCompetitorModels": [
          {
            "id": 123,
            "competitors": [
              {
                "name": "Matt Smith",
                "team": "Team Pole Queens"
              }
            ]
          }
        ]
      }
    ],
    "currentCompetitor": {
      "division": "Senior Women",
      "competitors": [
        {
          "name": "Matt Smith",
          "team": "Team Pole Queens"
        }
      ]
    }
  }
}
```

<h3 id="competitiongetcompetitionstatus-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|Success|[CompetitionStatusEnvelopeModel](#schemacompetitionstatusenvelopemodel)|

<aside class="success">
This operation does not require authentication
</aside>

## CompetitionDownloadCompetition

<a id="opIdCompetitionDownloadCompetition"></a>

> Code samples

```http
GET /Competition/download-competition HTTP/1.1

Accept: text/plain

```

`GET /Competition/download-competition`

*Returns competition status in file model.*

> Example responses

> 200 Response

```
{"name":"National finals 2022","divisions":[{"name":"Senior Women","items":[{"id":123,"competitors":[{"name":"Matt Smith","team":"Team Pole Queens"}],"forfeit":true,"results":{"artisticScore":59.266,"executionScore":70.333,"difficultyScore":12.8,"headJudgePenalty":0}}]}],"currentCompetitor":{"id":25,"division":"Senior Women","competitors":[{"name":"Matt Smith","team":"Team Pole Queens"}]}}
```

```json
{
  "name": "National finals 2022",
  "divisions": [
    {
      "name": "Senior Women",
      "items": [
        {
          "id": 123,
          "competitors": [
            {
              "name": "Matt Smith",
              "team": "Team Pole Queens"
            }
          ],
          "forfeit": true,
          "results": {
            "artisticScore": 59.266,
            "executionScore": 70.333,
            "difficultyScore": 12.8,
            "headJudgePenalty": 0
          }
        }
      ]
    }
  ],
  "currentCompetitor": {
    "id": 25,
    "division": "Senior Women",
    "competitors": [
      {
        "name": "Matt Smith",
        "team": "Team Pole Queens"
      }
    ]
  }
}
```

<h3 id="competitiondownloadcompetition-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|Success|[CompetitionFileModel](#schemacompetitionfilemodel)|

<aside class="success">
This operation does not require authentication
</aside>

<h1 id="competition-status-api-competitor">Competitor</h1>

## CompetitorGetAll

<a id="opIdCompetitorGetAll"></a>

> Code samples

```http
GET /Competitor/all HTTP/1.1

Accept: text/plain

```

`GET /Competitor/all`

> Example responses

> 200 Response

```
[{"division":"Senior Women","id":123,"competitors":[{"name":"Matt Smith","team":"Team Pole Queens"}],"result":{"total":127.266,"artisticScore":59.266,"executionScore":70.333,"difficultyScore":12.8,"headJudgePenalty":0},"forfeit":true}]
```

```json
[
  {
    "division": "Senior Women",
    "id": 123,
    "competitors": [
      {
        "name": "Matt Smith",
        "team": "Team Pole Queens"
      }
    ],
    "result": {
      "total": 127.266,
      "artisticScore": 59.266,
      "executionScore": 70.333,
      "difficultyScore": 12.8,
      "headJudgePenalty": 0
    },
    "forfeit": true
  }
]
```

<h3 id="competitorgetall-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|Success|Inline|

<h3 id="competitorgetall-responseschema">Response Schema</h3>

Status Code **200**

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|*anonymous*|[[ParticipationModel](#schemaparticipationmodel)]|false|none|[Represents a participation in division by one or multiple competitors.]|
|» division|string|true|none|Division name. Should match some division in currently active<br>competition|
|» id|integer(int32)|true|none|Unique ID for these comeptitors|
|» competitors|[[CompetitorModel](#schemacompetitormodel)]|true|none|Competitors|
|»» name|string¦null|false|none|Name of competitor|
|»» team|string¦null|false|none|Team of competitor|
|» result|[PoleSportResultModel](#schemapolesportresultmodel)|false|none|Represents a score in Pole Dance Sport series|
|»» total|number(double)|true|none|Calculated total score (A+E+D-HJ).<br>This is used to sort the score board.|
|»» artisticScore|number(double)|true|none|Artistic score (A)|
|»» executionScore|number(double)|true|none|Execution score (E)|
|»» difficultyScore|number(double)|true|none|Difficulty score (D)|
|»» headJudgePenalty|number(double)|true|none|Head judge penalty (HJ). This is subtracted from the total.|
|» forfeit|boolean|true|none|If true, competitors are shown as forfeited for this division.<br><br>In this context, forfeit can happen if:<br>a) Competitor doesn't show up for competition<br>b) Competitor gets injured and is unable to continue<br>c) Competitor is disqualified<br><br>This doesn't care if it's competitor's fault or not, this just<br>means that competitors score is not shown.<br><br>This means following:<br>a) Results are not shown, if given.<br>b) These competitors are not shown schedule.<br>c) When listed, competitors are shown in the bottom part of the<br>listing.|

<aside class="success">
This operation does not require authentication
</aside>

<h1 id="competition-status-api-home">Home</h1>

## HomeGetHealth

<a id="opIdHomeGetHealth"></a>

> Code samples

```http
GET / HTTP/1.1

```

`GET /`

<h3 id="homegethealth-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|Success|None|

<aside class="success">
This operation does not require authentication
</aside>

<h1 id="competition-status-api-scoreboard">Scoreboard</h1>

## ScoreboardGetStatus

<a id="opIdScoreboardGetStatus"></a>

> Code samples

```http
GET /Scoreboard HTTP/1.1

Accept: text/plain

```

`GET /Scoreboard`

> Example responses

> 200 Response

```
{"scoreboardMode":"Unknown","result":{"division":"Senior Women","currentPlace":1,"competitors":[{"name":"Matt Smith","team":"Team Pole Queens"}],"result":{"total":127.266,"artisticScore":59.266,"executionScore":70.333,"difficultyScore":12.8,"headJudgePenalty":0}},"upcomingCompetitors":[{"id":123,"competitors":[{"name":"Matt Smith","team":"Team Pole Queens"}]}]}
```

```json
{
  "scoreboardMode": "Unknown",
  "result": {
    "division": "Senior Women",
    "currentPlace": 1,
    "competitors": [
      {
        "name": "Matt Smith",
        "team": "Team Pole Queens"
      }
    ],
    "result": {
      "total": 127.266,
      "artisticScore": 59.266,
      "executionScore": 70.333,
      "difficultyScore": 12.8,
      "headJudgePenalty": 0
    }
  },
  "upcomingCompetitors": [
    {
      "id": 123,
      "competitors": [
        {
          "name": "Matt Smith",
          "team": "Team Pole Queens"
        }
      ]
    }
  ]
}
```

<h3 id="scoreboardgetstatus-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|Success|[ScoreboardStatusModel](#schemascoreboardstatusmodel)|

<aside class="success">
This operation does not require authentication
</aside>

## ScoreboardSetScoreboardMode

<a id="opIdScoreboardSetScoreboardMode"></a>

> Code samples

```http
PUT /Scoreboard/set-mode HTTP/1.1

```

`PUT /Scoreboard/set-mode`

<h3 id="scoreboardsetscoreboardmode-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|mode|query|[ScoreboardModeModel](#schemascoreboardmodemodel)|false|none|

#### Enumerated Values

|Parameter|Value|
|---|---|
|mode|Unknown|
|mode|DivisionStatus|
|mode|CompetitorResults|
|mode|UpcomingCompetitors|

<h3 id="scoreboardsetscoreboardmode-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|Success|None|

<aside class="success">
This operation does not require authentication
</aside>

## ScoreboardSelectResultForShowing

<a id="opIdScoreboardSelectResultForShowing"></a>

> Code samples

```http
PUT /Scoreboard/select-results HTTP/1.1

```

`PUT /Scoreboard/select-results`

*Sets results that will be shown. Doesn't show the results yet.
This is done with "set-mode"*

<h3 id="scoreboardselectresultforshowing-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|id|query|integer(int32)|false|id|

<h3 id="scoreboardselectresultforshowing-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|Success|None|

<aside class="success">
This operation does not require authentication
</aside>

## ScoreboardSetActiveDivision

<a id="opIdScoreboardSetActiveDivision"></a>

> Code samples

```http
PUT /Scoreboard/set-active-division HTTP/1.1

```

`PUT /Scoreboard/set-active-division`

<h3 id="scoreboardsetactivedivision-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|name|query|string|false|none|

<h3 id="scoreboardsetactivedivision-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|Success|None|

<aside class="success">
This operation does not require authentication
</aside>

<h1 id="competition-status-api-test">Test</h1>

## TestLoadPoleCompetitionData

<a id="opIdTestLoadPoleCompetitionData"></a>

> Code samples

```http
POST /Test/set-current-competitors HTTP/1.1

Content-Type: application/json

```

`POST /Test/set-current-competitors`

> Body parameter

```json
{
  "id": 0,
  "division": "string",
  "competitors": [
    {
      "name": "string",
      "team": "string"
    }
  ]
}
```

<h3 id="testloadpolecompetitiondata-parameters">Parameters</h3>

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|body|body|[CurrentCompetitorsEntity](#schemacurrentcompetitorsentity)|false|none|

<h3 id="testloadpolecompetitiondata-responses">Responses</h3>

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|Success|None|

<aside class="success">
This operation does not require authentication
</aside>

# Schemas

<h2 id="tocS_CompetitionFileModel">CompetitionFileModel</h2>
<!-- backwards compatibility -->
<a id="schemacompetitionfilemodel"></a>
<a id="schema_CompetitionFileModel"></a>
<a id="tocScompetitionfilemodel"></a>
<a id="tocscompetitionfilemodel"></a>

```json
{
  "name": "National finals 2022",
  "divisions": [
    {
      "name": "Senior Women",
      "items": [
        {
          "id": 123,
          "competitors": [
            {
              "name": "Matt Smith",
              "team": "Team Pole Queens"
            }
          ],
          "forfeit": true,
          "results": {
            "artisticScore": 59.266,
            "executionScore": 70.333,
            "difficultyScore": 12.8,
            "headJudgePenalty": 0
          }
        }
      ]
    }
  ],
  "currentCompetitor": {
    "id": 25,
    "division": "Senior Women",
    "competitors": [
      {
        "name": "Matt Smith",
        "team": "Team Pole Queens"
      }
    ]
  }
}

```

Describes the filemodel that is used to save current status of
competition. This model should hold all information of the competition
that can be saved to file.

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|name|string|true|none|Name of the whole competition.|
|divisions|[[DivisionFileModel](#schemadivisionfilemodel)]|true|none|Divisions for this competition.|
|currentCompetitor|[CurrentCompetitorFileModel](#schemacurrentcompetitorfilemodel)|false|none|Represents file model of current competitor performing or performing next<br>when no other competitor is not active.|

<h2 id="tocS_CompetitionStatusContentModel">CompetitionStatusContentModel</h2>
<!-- backwards compatibility -->
<a id="schemacompetitionstatuscontentmodel"></a>
<a id="schema_CompetitionStatusContentModel"></a>
<a id="tocScompetitionstatuscontentmodel"></a>
<a id="tocscompetitionstatuscontentmodel"></a>

```json
{
  "eventName": "National finals 2022",
  "createdAt": "2022-02-15T19:14:25.004Z",
  "divisions": [
    {
      "name": "Senior women",
      "results": [
        {
          "id": 0,
          "competitors": [
            {
              "name": "Matt Smith",
              "team": "Team Pole Queens"
            }
          ],
          "result": {
            "total": 127.266,
            "artisticScore": 59.266,
            "executionScore": 70.333,
            "difficultyScore": 12.8,
            "headJudgePenalty": 0
          },
          "forfeit": true
        }
      ],
      "forfeited": [
        {
          "id": 0,
          "competitors": [
            {
              "name": "Matt Smith",
              "team": "Team Pole Queens"
            }
          ],
          "result": {
            "total": 127.266,
            "artisticScore": 59.266,
            "executionScore": 70.333,
            "difficultyScore": 12.8,
            "headJudgePenalty": 0
          },
          "forfeit": true
        }
      ],
      "upcomingCompetitorModels": [
        {
          "id": 123,
          "competitors": [
            {
              "name": "Matt Smith",
              "team": "Team Pole Queens"
            }
          ]
        }
      ]
    }
  ],
  "currentCompetitor": {
    "division": "Senior Women",
    "competitors": [
      {
        "name": "Matt Smith",
        "team": "Team Pole Queens"
      }
    ]
  }
}

```

Current status of the competition.

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|eventName|string|true|none|Name of the event. Example: National finals 2022|
|createdAt|string|true|none|Timestamp indicating when this status was generated.<br>This is always In UTC<br>Format "yyyy-MM-ddTHH:mm:ss.fffZ"|
|divisions|[[DivisionStatusModel](#schemadivisionstatusmodel)]|true|none|Current status of divisions|
|currentCompetitor|[CurrentCompetitorContentModel](#schemacurrentcompetitorcontentmodel)|false|none|Represents current competitor who is performing or performing next<br>when no other competitor is not active.|

<h2 id="tocS_CompetitionStatusEnvelopeModel">CompetitionStatusEnvelopeModel</h2>
<!-- backwards compatibility -->
<a id="schemacompetitionstatusenvelopemodel"></a>
<a id="schema_CompetitionStatusEnvelopeModel"></a>
<a id="tocScompetitionstatusenvelopemodel"></a>
<a id="tocscompetitionstatusenvelopemodel"></a>

```json
{
  "version": "string",
  "type": "string",
  "content": {
    "eventName": "National finals 2022",
    "createdAt": "2022-02-15T19:14:25.004Z",
    "divisions": [
      {
        "name": "Senior women",
        "results": [
          {
            "id": 0,
            "competitors": [
              {
                "name": "Matt Smith",
                "team": "Team Pole Queens"
              }
            ],
            "result": {
              "total": 127.266,
              "artisticScore": 59.266,
              "executionScore": 70.333,
              "difficultyScore": 12.8,
              "headJudgePenalty": 0
            },
            "forfeit": true
          }
        ],
        "forfeited": [
          {
            "id": 0,
            "competitors": [
              {
                "name": "Matt Smith",
                "team": "Team Pole Queens"
              }
            ],
            "result": {
              "total": 127.266,
              "artisticScore": 59.266,
              "executionScore": 70.333,
              "difficultyScore": 12.8,
              "headJudgePenalty": 0
            },
            "forfeit": true
          }
        ],
        "upcomingCompetitorModels": [
          {
            "id": 123,
            "competitors": [
              {
                "name": "Matt Smith",
                "team": "Team Pole Queens"
              }
            ]
          }
        ]
      }
    ],
    "currentCompetitor": {
      "division": "Senior Women",
      "competitors": [
        {
          "name": "Matt Smith",
          "team": "Team Pole Queens"
        }
      ]
    }
  }
}

```

Contains current status of competition, of competition is active.
If competition is not active, Content can be null.

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|version|string|true|read-only|Envelope version number. Version can be discarded if no<br>functionality is specified for given version|
|type|string|true|read-only|Type of the message. This and version can be used to identify<br>correct parser for this message.|
|content|[CompetitionStatusContentModel](#schemacompetitionstatuscontentmodel)|false|none|Current status of the competition.|

<h2 id="tocS_CompetitorEntity">CompetitorEntity</h2>
<!-- backwards compatibility -->
<a id="schemacompetitorentity"></a>
<a id="schema_CompetitorEntity"></a>
<a id="tocScompetitorentity"></a>
<a id="tocscompetitorentity"></a>

```json
{
  "name": "string",
  "team": "string"
}

```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|name|string¦null|false|none|none|
|team|string¦null|false|none|none|

<h2 id="tocS_CompetitorFileModel">CompetitorFileModel</h2>
<!-- backwards compatibility -->
<a id="schemacompetitorfilemodel"></a>
<a id="schema_CompetitorFileModel"></a>
<a id="tocScompetitorfilemodel"></a>
<a id="tocscompetitorfilemodel"></a>

```json
{
  "name": "Matt Smith",
  "team": "Team Pole Queens"
}

```

Represents a single competitor in file. May be a part of team.

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|name|string|true|none|Name of the competitor|
|team|string¦null|false|none|Team, if given.|

<h2 id="tocS_CompetitorModel">CompetitorModel</h2>
<!-- backwards compatibility -->
<a id="schemacompetitormodel"></a>
<a id="schema_CompetitorModel"></a>
<a id="tocScompetitormodel"></a>
<a id="tocscompetitormodel"></a>

```json
{
  "name": "Matt Smith",
  "team": "Team Pole Queens"
}

```

Represents a single competitor.

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|name|string¦null|false|none|Name of competitor|
|team|string¦null|false|none|Team of competitor|

<h2 id="tocS_CompetitorPositionFileModel">CompetitorPositionFileModel</h2>
<!-- backwards compatibility -->
<a id="schemacompetitorpositionfilemodel"></a>
<a id="schema_CompetitorPositionFileModel"></a>
<a id="tocScompetitorpositionfilemodel"></a>
<a id="tocscompetitorpositionfilemodel"></a>

```json
{
  "id": 123,
  "competitors": [
    {
      "name": "Matt Smith",
      "team": "Team Pole Queens"
    }
  ],
  "forfeit": true,
  "results": {
    "artisticScore": 59.266,
    "executionScore": 70.333,
    "difficultyScore": 12.8,
    "headJudgePenalty": 0
  }
}

```

Represents a competitor or competitors (if doubles or other team
activity) who participate in division. This class describes what is the
status of their participation in the division.

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|id|integer(int32)|true|none|Unique ID for these competitors. This is asystem ID, which is used<br>to set the competitor(s) as active competitor.<br>NOTE: This is not the same as a jersey number etc.|
|competitors|[[CompetitorFileModel](#schemacompetitorfilemodel)]|true|none|Competitors, should have at least one entity.|
|forfeit|boolean|true|none|If true, competitors are shown as forfeited for this division.<br><br>In this context, forfeit can happen if:<br>a) Competitor doesn't show up for competition<br>b) Competitor gets injured and is unable to continue<br>c) Competitor is disqualified<br><br>This doesn't care if it's competitor's fault or not, this just<br>means that competitors score is not shown.<br><br>This means following:<br>a) Results are not shown, if given.<br>b) These competitors are not shown schedule.<br>c) When listed, competitors are shown in the bottom part of the<br>listing.|
|results|[PoleResultFileModel](#schemapoleresultfilemodel)|false|none|Represents a score in Pole Dance Sport series|

<h2 id="tocS_CompetitorResultModel">CompetitorResultModel</h2>
<!-- backwards compatibility -->
<a id="schemacompetitorresultmodel"></a>
<a id="schema_CompetitorResultModel"></a>
<a id="tocScompetitorresultmodel"></a>
<a id="tocscompetitorresultmodel"></a>

```json
{
  "id": 2,
  "results": {
    "artisticScore": 59.266,
    "executionScore": 70.333,
    "difficultyScore": 12.8,
    "headJudgePenalty": 0
  }
}

```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|id|integer(int32)|true|none|ID of competitor whose results are set|
|results|[PoleResultFileModel](#schemapoleresultfilemodel)|false|none|Represents a score in Pole Dance Sport series|

<h2 id="tocS_CurrentCompetitorContentModel">CurrentCompetitorContentModel</h2>
<!-- backwards compatibility -->
<a id="schemacurrentcompetitorcontentmodel"></a>
<a id="schema_CurrentCompetitorContentModel"></a>
<a id="tocScurrentcompetitorcontentmodel"></a>
<a id="tocscurrentcompetitorcontentmodel"></a>

```json
{
  "division": "Senior Women",
  "competitors": [
    {
      "name": "Matt Smith",
      "team": "Team Pole Queens"
    }
  ]
}

```

Represents current competitor who is performing or performing next
when no other competitor is not active.

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|division|string|true|none|Division of competitor(s). This should match some division in active<br>competition|
|competitors|[[CompetitorModel](#schemacompetitormodel)]|true|none|Competitor(s). This should have at least one value, but may have<br>multiple values if there are multiple persons performing for single<br>performance.|

<h2 id="tocS_CurrentCompetitorEnvelopeModel">CurrentCompetitorEnvelopeModel</h2>
<!-- backwards compatibility -->
<a id="schemacurrentcompetitorenvelopemodel"></a>
<a id="schema_CurrentCompetitorEnvelopeModel"></a>
<a id="tocScurrentcompetitorenvelopemodel"></a>
<a id="tocscurrentcompetitorenvelopemodel"></a>

```json
{
  "version": "string",
  "type": "string",
  "content": {
    "division": "Senior Women",
    "competitors": [
      {
        "name": "Matt Smith",
        "team": "Team Pole Queens"
      }
    ]
  }
}

```

Envelope for current competitor. Current competitor is the competitor that
is performing currently or is performing next, if no one is performing.

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|version|string|true|read-only|Envelope version number. Version can be discarded if no<br>functionality is specified for given version|
|type|string|true|read-only|Type of the message. This and version can be used to identify<br>correct parser for this message.|
|content|[CurrentCompetitorContentModel](#schemacurrentcompetitorcontentmodel)|false|none|Represents current competitor who is performing or performing next<br>when no other competitor is not active.|

<h2 id="tocS_CurrentCompetitorFileModel">CurrentCompetitorFileModel</h2>
<!-- backwards compatibility -->
<a id="schemacurrentcompetitorfilemodel"></a>
<a id="schema_CurrentCompetitorFileModel"></a>
<a id="tocScurrentcompetitorfilemodel"></a>
<a id="tocscurrentcompetitorfilemodel"></a>

```json
{
  "id": 25,
  "division": "Senior Women",
  "competitors": [
    {
      "name": "Matt Smith",
      "team": "Team Pole Queens"
    }
  ]
}

```

Represents file model of current competitor performing or performing next
when no other competitor is not active.

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|id|integer(int32)¦null|false|none|ID of competitor. This might not be set if competitor is not listed.|
|division|string|true|none|Division of competitor(s). This should match some division in<br>current competition.|
|competitors|[[CompetitorFileModel](#schemacompetitorfilemodel)]|true|none|Competitor(s). This should have at least one value, but may have<br>multiple values if there are multiple persons performing for single<br>performance.|

<h2 id="tocS_CurrentCompetitorsEntity">CurrentCompetitorsEntity</h2>
<!-- backwards compatibility -->
<a id="schemacurrentcompetitorsentity"></a>
<a id="schema_CurrentCompetitorsEntity"></a>
<a id="tocScurrentcompetitorsentity"></a>
<a id="tocscurrentcompetitorsentity"></a>

```json
{
  "id": 0,
  "division": "string",
  "competitors": [
    {
      "name": "string",
      "team": "string"
    }
  ]
}

```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|id|integer(int32)¦null|false|none|none|
|division|string¦null|false|none|none|
|competitors|[[CompetitorEntity](#schemacompetitorentity)]¦null|false|none|none|

<h2 id="tocS_CurrentCompetitorSetModel">CurrentCompetitorSetModel</h2>
<!-- backwards compatibility -->
<a id="schemacurrentcompetitorsetmodel"></a>
<a id="schema_CurrentCompetitorSetModel"></a>
<a id="tocScurrentcompetitorsetmodel"></a>
<a id="tocscurrentcompetitorsetmodel"></a>

```json
{
  "id": 123
}

```

Model used to set ID of current competitor(s)

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|id|integer(int32)¦null|false|none|ID of currently active competitor (or who is performing next if no one is active).<br><br>If null is used, active competitor is cleared.|

<h2 id="tocS_DivisionFileModel">DivisionFileModel</h2>
<!-- backwards compatibility -->
<a id="schemadivisionfilemodel"></a>
<a id="schema_DivisionFileModel"></a>
<a id="tocSdivisionfilemodel"></a>
<a id="tocsdivisionfilemodel"></a>

```json
{
  "name": "Senior Women",
  "items": [
    {
      "id": 123,
      "competitors": [
        {
          "name": "Matt Smith",
          "team": "Team Pole Queens"
        }
      ],
      "forfeit": true,
      "results": {
        "artisticScore": 59.266,
        "executionScore": 70.333,
        "difficultyScore": 12.8,
        "headJudgePenalty": 0
      }
    }
  ]
}

```

Describes division that is saved in file. User for JSON conversions.

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|name|string|true|none|Name of the division.|
|items|[[CompetitorPositionFileModel](#schemacompetitorpositionfilemodel)]|true|none|Competitors participating in this division.|

<h2 id="tocS_DivisionStatusModel">DivisionStatusModel</h2>
<!-- backwards compatibility -->
<a id="schemadivisionstatusmodel"></a>
<a id="schema_DivisionStatusModel"></a>
<a id="tocSdivisionstatusmodel"></a>
<a id="tocsdivisionstatusmodel"></a>

```json
{
  "name": "Senior women",
  "results": [
    {
      "id": 0,
      "competitors": [
        {
          "name": "Matt Smith",
          "team": "Team Pole Queens"
        }
      ],
      "result": {
        "total": 127.266,
        "artisticScore": 59.266,
        "executionScore": 70.333,
        "difficultyScore": 12.8,
        "headJudgePenalty": 0
      },
      "forfeit": true
    }
  ],
  "forfeited": [
    {
      "id": 0,
      "competitors": [
        {
          "name": "Matt Smith",
          "team": "Team Pole Queens"
        }
      ],
      "result": {
        "total": 127.266,
        "artisticScore": 59.266,
        "executionScore": 70.333,
        "difficultyScore": 12.8,
        "headJudgePenalty": 0
      },
      "forfeit": true
    }
  ],
  "upcomingCompetitorModels": [
    {
      "id": 123,
      "competitors": [
        {
          "name": "Matt Smith",
          "team": "Team Pole Queens"
        }
      ]
    }
  ]
}

```

This represens a current status for a division.
This is generated on the fly.

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|name|string|true|none|Name if the division. Example: "Senior women"|
|results|[[ParticipationRowModel](#schemaparticipationrowmodel)]|true|none|Current results in order. Forfeited are not yet listed|
|forfeited|[[ParticipationRowModel](#schemaparticipationrowmodel)]|true|none|Forfeited competitors. Is empty if no one has forfeited. These are<br>not returned in any special order.|
|upcomingCompetitorModels|[[UpcomingCompetitorModel](#schemaupcomingcompetitormodel)]|true|none|Upcoming competitors. First is in zero index.<br>This can be empty, if no competitors are remaining.|

<h2 id="tocS_ParticipationModel">ParticipationModel</h2>
<!-- backwards compatibility -->
<a id="schemaparticipationmodel"></a>
<a id="schema_ParticipationModel"></a>
<a id="tocSparticipationmodel"></a>
<a id="tocsparticipationmodel"></a>

```json
{
  "division": "Senior Women",
  "id": 123,
  "competitors": [
    {
      "name": "Matt Smith",
      "team": "Team Pole Queens"
    }
  ],
  "result": {
    "total": 127.266,
    "artisticScore": 59.266,
    "executionScore": 70.333,
    "difficultyScore": 12.8,
    "headJudgePenalty": 0
  },
  "forfeit": true
}

```

Represents a participation in division by one or multiple competitors.

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|division|string|true|none|Division name. Should match some division in currently active<br>competition|
|id|integer(int32)|true|none|Unique ID for these comeptitors|
|competitors|[[CompetitorModel](#schemacompetitormodel)]|true|none|Competitors|
|result|[PoleSportResultModel](#schemapolesportresultmodel)|false|none|Represents a score in Pole Dance Sport series|
|forfeit|boolean|true|none|If true, competitors are shown as forfeited for this division.<br><br>In this context, forfeit can happen if:<br>a) Competitor doesn't show up for competition<br>b) Competitor gets injured and is unable to continue<br>c) Competitor is disqualified<br><br>This doesn't care if it's competitor's fault or not, this just<br>means that competitors score is not shown.<br><br>This means following:<br>a) Results are not shown, if given.<br>b) These competitors are not shown schedule.<br>c) When listed, competitors are shown in the bottom part of the<br>listing.|

<h2 id="tocS_ParticipationRowModel">ParticipationRowModel</h2>
<!-- backwards compatibility -->
<a id="schemaparticipationrowmodel"></a>
<a id="schema_ParticipationRowModel"></a>
<a id="tocSparticipationrowmodel"></a>
<a id="tocsparticipationrowmodel"></a>

```json
{
  "id": 0,
  "competitors": [
    {
      "name": "Matt Smith",
      "team": "Team Pole Queens"
    }
  ],
  "result": {
    "total": 127.266,
    "artisticScore": 59.266,
    "executionScore": 70.333,
    "difficultyScore": 12.8,
    "headJudgePenalty": 0
  },
  "forfeit": true
}

```

This represents a finished or or otherwise resolved performance result.
Result might be missing, if performance has been finished but has not been
graded yet or if the competitor has forfeited.

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|id|integer(int32)|true|none|Unique ID for these comeptitors|
|competitors|[[CompetitorModel](#schemacompetitormodel)]|true|none|Competitors|
|result|[PoleSportResultModel](#schemapolesportresultmodel)|false|none|Represents a score in Pole Dance Sport series|
|forfeit|boolean|true|none|If true, competitors are shown as forfeited for this division.<br><br>In this context, forfeit can happen if:<br>a) Competitor doesn't show up for competition<br>b) Competitor gets injured and is unable to continue<br>c) Competitor is disqualified<br><br>This doesn't care if it's competitor's fault or not, this just<br>means that competitors score is not shown.<br><br>This means following:<br>a) Results are not shown, if given.<br>b) These competitors are not shown schedule.<br>c) When listed, competitors are shown in the bottom part of the<br>listing.|

<h2 id="tocS_PerformanceResultsContentModel">PerformanceResultsContentModel</h2>
<!-- backwards compatibility -->
<a id="schemaperformanceresultscontentmodel"></a>
<a id="schema_PerformanceResultsContentModel"></a>
<a id="tocSperformanceresultscontentmodel"></a>
<a id="tocsperformanceresultscontentmodel"></a>

```json
{
  "division": "Senior Women",
  "currentPlace": 1,
  "competitors": [
    {
      "name": "Matt Smith",
      "team": "Team Pole Queens"
    }
  ],
  "result": {
    "total": 127.266,
    "artisticScore": 59.266,
    "executionScore": 70.333,
    "difficultyScore": 12.8,
    "headJudgePenalty": 0
  }
}

```

Describes a result for competitor(s) in for a single performance and what
place it did achieve, if any.

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|division|string|true|none|Name of the division. Should match some division in currenlty active<br>competition.|
|currentPlace|integer(int32)¦null|false|none|Placement that the competitor(s) received with this result. This is null<br>If current place couldn't be calculated.<br>Starts from 1.|
|competitors|[[CompetitorModel](#schemacompetitormodel)]|true|none|Competitors that did the performance. This should contain at least one<br>item.|
|result|[PoleSportResultModel](#schemapolesportresultmodel)|true|none|Represents a score in Pole Dance Sport series|

<h2 id="tocS_PerformanceResultsEnvelopeModel">PerformanceResultsEnvelopeModel</h2>
<!-- backwards compatibility -->
<a id="schemaperformanceresultsenvelopemodel"></a>
<a id="schema_PerformanceResultsEnvelopeModel"></a>
<a id="tocSperformanceresultsenvelopemodel"></a>
<a id="tocsperformanceresultsenvelopemodel"></a>

```json
{
  "version": "string",
  "type": "string",
  "content": {
    "division": "Senior Women",
    "currentPlace": 1,
    "competitors": [
      {
        "name": "Matt Smith",
        "team": "Team Pole Queens"
      }
    ],
    "result": {
      "total": 127.266,
      "artisticScore": 59.266,
      "executionScore": 70.333,
      "difficultyScore": 12.8,
      "headJudgePenalty": 0
    }
  }
}

```

Message containing performance results

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|version|string|true|read-only|Envelope version number. Version can be discarded if no<br>functionality is specified for given version|
|type|string|true|read-only|Type of the message. This and version can be used to identify<br>correct parser for this message.|
|content|[PerformanceResultsContentModel](#schemaperformanceresultscontentmodel)|false|none|Describes a result for competitor(s) in for a single performance and what<br>place it did achieve, if any.|

<h2 id="tocS_PoleResultFileModel">PoleResultFileModel</h2>
<!-- backwards compatibility -->
<a id="schemapoleresultfilemodel"></a>
<a id="schema_PoleResultFileModel"></a>
<a id="tocSpoleresultfilemodel"></a>
<a id="tocspoleresultfilemodel"></a>

```json
{
  "artisticScore": 59.266,
  "executionScore": 70.333,
  "difficultyScore": 12.8,
  "headJudgePenalty": 0
}

```

Represents a score in Pole Dance Sport series

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|artisticScore|number(double)|true|none|Artistic score (A)|
|executionScore|number(double)|true|none|Execution score (E)|
|difficultyScore|number(double)|true|none|Difficulty score (D)|
|headJudgePenalty|number(double)|true|none|Head judge penalty (HJ). This is subtracted from the total.|

<h2 id="tocS_PoleSportResultModel">PoleSportResultModel</h2>
<!-- backwards compatibility -->
<a id="schemapolesportresultmodel"></a>
<a id="schema_PoleSportResultModel"></a>
<a id="tocSpolesportresultmodel"></a>
<a id="tocspolesportresultmodel"></a>

```json
{
  "total": 127.266,
  "artisticScore": 59.266,
  "executionScore": 70.333,
  "difficultyScore": 12.8,
  "headJudgePenalty": 0
}

```

Represents a score in Pole Dance Sport series

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|total|number(double)|true|none|Calculated total score (A+E+D-HJ).<br>This is used to sort the score board.|
|artisticScore|number(double)|true|none|Artistic score (A)|
|executionScore|number(double)|true|none|Execution score (E)|
|difficultyScore|number(double)|true|none|Difficulty score (D)|
|headJudgePenalty|number(double)|true|none|Head judge penalty (HJ). This is subtracted from the total.|

<h2 id="tocS_ScoreboardModeModel">ScoreboardModeModel</h2>
<!-- backwards compatibility -->
<a id="schemascoreboardmodemodel"></a>
<a id="schema_ScoreboardModeModel"></a>
<a id="tocSscoreboardmodemodel"></a>
<a id="tocsscoreboardmodemodel"></a>

```json
"Unknown"

```

This desribes the mode that the scoreboard should be in.

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|*anonymous*|string|false|none|This desribes the mode that the scoreboard should be in.|

#### Enumerated Values

|Property|Value|
|---|---|
|*anonymous*|Unknown|
|*anonymous*|DivisionStatus|
|*anonymous*|CompetitorResults|
|*anonymous*|UpcomingCompetitors|

<h2 id="tocS_ScoreboardStatusModel">ScoreboardStatusModel</h2>
<!-- backwards compatibility -->
<a id="schemascoreboardstatusmodel"></a>
<a id="schema_ScoreboardStatusModel"></a>
<a id="tocSscoreboardstatusmodel"></a>
<a id="tocsscoreboardstatusmodel"></a>

```json
{
  "scoreboardMode": "Unknown",
  "result": {
    "division": "Senior Women",
    "currentPlace": 1,
    "competitors": [
      {
        "name": "Matt Smith",
        "team": "Team Pole Queens"
      }
    ],
    "result": {
      "total": 127.266,
      "artisticScore": 59.266,
      "executionScore": 70.333,
      "difficultyScore": 12.8,
      "headJudgePenalty": 0
    }
  },
  "upcomingCompetitors": [
    {
      "id": 123,
      "competitors": [
        {
          "name": "Matt Smith",
          "team": "Team Pole Queens"
        }
      ]
    }
  ]
}

```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|scoreboardMode|[ScoreboardModeModel](#schemascoreboardmodemodel)|true|none|This desribes the mode that the scoreboard should be in.|
|result|[PerformanceResultsContentModel](#schemaperformanceresultscontentmodel)|false|none|Describes a result for competitor(s) in for a single performance and what<br>place it did achieve, if any.|
|upcomingCompetitors|[[UpcomingCompetitorModel](#schemaupcomingcompetitormodel)]|true|none|[Represents an upcoming competitor.]|

<h2 id="tocS_UpcomingCompetitorModel">UpcomingCompetitorModel</h2>
<!-- backwards compatibility -->
<a id="schemaupcomingcompetitormodel"></a>
<a id="schema_UpcomingCompetitorModel"></a>
<a id="tocSupcomingcompetitormodel"></a>
<a id="tocsupcomingcompetitormodel"></a>

```json
{
  "id": 123,
  "competitors": [
    {
      "name": "Matt Smith",
      "team": "Team Pole Queens"
    }
  ]
}

```

Represents an upcoming competitor.

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|id|integer(int32)|true|none|Unique ID for these competitors|
|competitors|[[CompetitorModel](#schemacompetitormodel)]|true|none|Competitor(s). Contains at least one entity.|

