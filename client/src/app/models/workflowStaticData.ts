import { Observable, from } from 'rxjs';
import { WorkFlow } from './workflow.model';

export const workflowTemplateRecord : WorkFlow =

    {
      "actions": [
          {
            "api": "API",
            "title": "Action Title",
            "type": "Action.Submit"
          }
        ],

      "template": {
        "description": "Missed Closed Date",
        "title": "",
        "type": "",
        "recipientsTo": "",
        "recipientsCc": "",
        "recipientsBcc": ""
      },
      "frequencyTime": "Sat, 16 Apr 2022 09:22:09 GMT",
      "workflowName": "Missed Closed Date",
      "frequency": "",
      "isActive": true,
      "workflowDesc": "Summary",
      "id": 1,
       "adaptiveCardJSON": ""
    }



