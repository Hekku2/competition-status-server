### [Api.Models](Api_Models.md 'Api.Models')
## ParticipationModel Class
Represents a participation in division by one or multiple competitors.  
```csharp
public class ParticipationModel
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; ParticipationModel  

| Properties | |
| :--- | :--- |
| [Competitors](Api_Models_ParticipationModel_Competitors.md 'Api.Models.ParticipationModel.Competitors') | Competitors<br/> |
| [Division](Api_Models_ParticipationModel_Division.md 'Api.Models.ParticipationModel.Division') | Division name<br/> |
| [Forfeit](Api_Models_ParticipationModel_Forfeit.md 'Api.Models.ParticipationModel.Forfeit') | If true, this competitor has forfeited and should not have a result<br/> |
| [Id](Api_Models_ParticipationModel_Id.md 'Api.Models.ParticipationModel.Id') | Unique ID for these comeptitors<br/> |
| [Result](Api_Models_ParticipationModel_Result.md 'Api.Models.ParticipationModel.Result') | Result for the competitors. This can be missing if competitor<br/>has not yet received it, or if competitor has forfeited.<br/> |
