# Consolidation_API
## Members of the team
- **[Loïc Rico](https://github.com/ricoloic)**

- **[Anthony Pageau](https://github.com/ricoloic)**

- **[Jean-Francois Taillefer](https://github.com/ricoloic)**

- **[Louis-Felix Beland](https://github.com/ricoloic)**

- **[Alimourana Balde](https://github.com/alimourana)**
- This is a modification of the original  Rest API made by the team to accommodate new requests  .
-  The new additions were made by : **[Louis-Felix Beland](https://github.com/ricoloic)**

## How to find and manipulate the status of all the relevant entities of the operational database
 You can do it manually with the walkthrough below.

1- Retrieving all the intervention with pending status:

    GET 
    https://louisconsolidation.azurewebsites.net/api/Intervention
    SEND
    
2- Changing the status of a specific Intervention's status to "InProgress":

    PUT 
    https://louisconsolidation.azurewebsites.net/api/Intervention/InProgress/1	[1 = Intervention ID]
    Select:	 Body
                Raw
                JSON
    In the text field, enter: { "intervention_status": "InProgress" } 
    SEND
   3- Changing the status of a specific Intervention's status to "Completed":

    PUT 
    https://louisconsolidation.azurewebsites.net/api/Intervention/Completed/1	[1 = Intervention ID]
    Select:	 Body
                Raw
                JSON
    In the text field, enter: { "intervention_status": "Completed" } 
    SEND
