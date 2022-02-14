### [Api.Models](Api_Models.md 'Api.Models')
## PerformanceResultsContentModel Class
Describes a result for competitor(s) in for a single performance and what  
place it did achieve, if any.  
```csharp
public class PerformanceResultsContentModel
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; PerformanceResultsContentModel  

| Properties | |
| :--- | :--- |
| [Competitors](Api_Models_PerformanceResultsContentModel_Competitors.md 'Api.Models.PerformanceResultsContentModel.Competitors') | Competitors that did the performance. This should contain at least one<br/>item.<br/> |
| [CurrentPlace](Api_Models_PerformanceResultsContentModel_CurrentPlace.md 'Api.Models.PerformanceResultsContentModel.CurrentPlace') | Placement that the competitor(s) received with this result. This is null<br/>If current place couldn't be calculated.<br/> |
| [Division](Api_Models_PerformanceResultsContentModel_Division.md 'Api.Models.PerformanceResultsContentModel.Division') | Name of the division. Example: Senior women<br/> |
| [Result](Api_Models_PerformanceResultsContentModel_Result.md 'Api.Models.PerformanceResultsContentModel.Result') | Result/score of the performance.<br/> |
