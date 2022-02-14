### [Api.Models](Api_Models.md 'Api.Models')
## DivisionStatusModel Class
This represens a current status for a division.  
This is generated on the fly.  
```csharp
public class DivisionStatusModel
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; DivisionStatusModel  

| Properties | |
| :--- | :--- |
| [Forfeited](Api_Models_DivisionStatusModel_Forfeited.md 'Api.Models.DivisionStatusModel.Forfeited') | Forfeited competitors. Is empty if no one has forfeited.<br/> |
| [Name](Api_Models_DivisionStatusModel_Name.md 'Api.Models.DivisionStatusModel.Name') | Name if the division. Example: "Senior women"<br/> |
| [Results](Api_Models_DivisionStatusModel_Results.md 'Api.Models.DivisionStatusModel.Results') | Current results in order. Forfeited are not yet listed<br/> |
| [UpcomingCompetitorModels](Api_Models_DivisionStatusModel_UpcomingCompetitorModels.md 'Api.Models.DivisionStatusModel.UpcomingCompetitorModels') | Upcoming competitors. First is in zero index.<br/>This can be empty, if no competitors are remaining.<br/> |
