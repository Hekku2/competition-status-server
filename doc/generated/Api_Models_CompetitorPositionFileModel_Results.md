### [Api.Models](Api_Models.md 'Api.Models').[CompetitorPositionFileModel](Api_Models_CompetitorPositionFileModel.md 'Api.Models.CompetitorPositionFileModel')
## CompetitorPositionFileModel.Results Property
Results, if competitor has already performed and received result.  
  
If this is set, competitor is no longer shown in "coming next"  
section and is instead shown on the score board.  
```csharp
public Api.Models.PoleResultFileModel? Results { get; set; }
```
#### Property Value
[PoleResultFileModel](Api_Models_PoleResultFileModel.md 'Api.Models.PoleResultFileModel')
