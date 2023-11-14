import { AzureFunction, Context, HttpRequest } from "@azure/functions";
import Openfort from "@openfort/openfort-node";

const openfort = new Openfort(process.env.OF_API_KEY);

function isValidRequestBody(body: any): boolean {
  return body?.CallerEntityProfile?.Lineage?.MasterPlayerAccountId &&
         body?.FunctionArgument?.connectionId
}

const httpTrigger: AzureFunction = async function (context: Context, req: HttpRequest): Promise<void> {
  try {
    context.log("Starting HTTP trigger function processing.");

    if (!isValidRequestBody(req.body)) {
      context.log("Invalid request body received.");
      context.res = { status: 400, body: "Please pass a valid request body" };
      return;
    }

    const connectionId = req.body.FunctionArgument.connectionId;
    const response = await openfort.web3Connections.getWeb3Actions({ id: connectionId });

    if (!response) {
      context.res = { status: 204, body: "No content received from Openfort API." };
      return;
    }

    context.res = { status: 200, body: JSON.stringify(response) };
    context.log("API call was successful and response sent.");
  } catch (error) {
    context.log("Unhandled error occurred:", error);
    context.res = { status: 500, body: JSON.stringify(error) };
  }
};

export default httpTrigger;