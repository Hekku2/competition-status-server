# Object descriptions

This file describes different object schemas used by this software.

## Competitor

Competitor object contains information which identifies the competitor.
This doesn't include current series, results, etc.

Fields:

* **name** Name of the competitors, starting with last name and separated by
comma.
* **team** Name of the team. This is not required.
* **competitor-id** An identification (usually a number) for competitor.
This field may be null or empty if not used.

Example


Example
```json
{
    "name": "last name, first name",
    "team": "sport team here",
    "competitor-id": "123"
}
```

## Pole sport result

A result for pole sport performance.

Fields:

* **total** Total score
* **a** Artistic element
* **d** Difficulty element
* **e** Execution element
* **hj** Head judge penalties

Example
```json
{
    "total": 123.00,
    "a": 100.457,
    "e": 100.457,
    "d": 9.4,
    "hj": 4.0
}
```

## Single category result

A result for a single try in single category.
Not used in pole sports

```json
{
    "category": "hauling",
    "attempt": 1,
    "result": "result"
}`
```

## VahvinSM and Otevoima result

TODO: Find out categroy names

Fields:

* **category 1** Score for category 1

```json
{
    "sport1": 123,
    "sport2": 123,
    "sport3": 123,
    "sport4": 123,
}
```
