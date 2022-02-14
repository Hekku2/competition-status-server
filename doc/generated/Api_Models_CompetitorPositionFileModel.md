### [Api.Models](Api_Models.md 'Api.Models')
## CompetitorPositionFileModel Class
Represents a competitor or competitors (if doubles or other team  
activity) who participate in division. This class describes what is the  
status of their participation in the division.  
```csharp
public class CompetitorPositionFileModel
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; CompetitorPositionFileModel  

| Properties | |
| :--- | :--- |
| [Competitors](Api_Models_CompetitorPositionFileModel_Competitors.md 'Api.Models.CompetitorPositionFileModel.Competitors') | Competitors, should have at least one entity.<br/> |
| [Forfeit](Api_Models_CompetitorPositionFileModel_Forfeit.md 'Api.Models.CompetitorPositionFileModel.Forfeit') | If true, competitors are shown as forfeited for this division.<br/>This means following:<br/>a) Results are not shown, if given.<br/>b) These competitors are not shown schedule.<br/>c) When listed, competitors are shown in the bottom part of the<br/>listing.<br/> |
| [Id](Api_Models_CompetitorPositionFileModel_Id.md 'Api.Models.CompetitorPositionFileModel.Id') | Unique ID for these competitors. This is asystem ID, which is used<br/>to set the competitor(s) as active competitor.<br/>NOTE: This is not the same as a jersey number etc.<br/> |
| [Results](Api_Models_CompetitorPositionFileModel_Results.md 'Api.Models.CompetitorPositionFileModel.Results') | Results, if competitor has already performed and received result.<br/><br/>If this is set, competitor is no longer shown in "coming next"<br/>section and is instead shown on the score board.<br/> |
