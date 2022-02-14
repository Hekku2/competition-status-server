### [Api.Models](Api_Models.md 'Api.Models')
## ResultRowModel Class
This represents a finished or or otherwise resolved performance result.  
Result might be missing, if performance has been finished but has not been  
graded yet or if the competitor has forfeited.  
```csharp
public class ResultRowModel
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; ResultRowModel  

| Properties | |
| :--- | :--- |
| [Competitors](Api_Models_ResultRowModel_Competitors.md 'Api.Models.ResultRowModel.Competitors') | Competitors<br/> |
| [Forfeit](Api_Models_ResultRowModel_Forfeit.md 'Api.Models.ResultRowModel.Forfeit') | If true, this competitor has forfeited and should not have a result<br/> |
| [Id](Api_Models_ResultRowModel_Id.md 'Api.Models.ResultRowModel.Id') | Unique ID for these comeptitors<br/> |
| [Result](Api_Models_ResultRowModel_Result.md 'Api.Models.ResultRowModel.Result') | Result for the competitors. This can be missing if competitor<br/>has not yet received it, or if competitor has forfeited.<br/> |
