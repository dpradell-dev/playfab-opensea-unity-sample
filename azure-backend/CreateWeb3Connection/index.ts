import { AzureFunction, Context, HttpRequest } from "@azure/functions";
import Openfort from "@openfort/openfort-node";

const openfort = new Openfort(process.env.OF_API_KEY);

function isValidRequestBody(body: any): boolean {
  return body &&
    body.CallerEntityProfile &&
    body.CallerEntityProfile.Lineage &&
    body.CallerEntityProfile.Lineage.MasterPlayerAccountId &&
    body.FunctionArgument &&
    body.FunctionArgument.playerId &&
    body.FunctionArgument.chainId &&
    body.FunctionArgument.uri
}

const httpTrigger: AzureFunction = async function (
  context: Context,
  req: HttpRequest
): Promise<void> {
  try {
    context.log("Starting HTTP trigger function processing.");

    if (!isValidRequestBody(req.body)) {
      context.log("Invalid request body received.");
      context.res = {
        status: 400,
        body: "Please pass a valid request body",
      };
      return;
    }

    const playerId = req.body.FunctionArgument.playerId;
    const chainId = req.body.FunctionArgument.chainId;
    const uri = req.body.FunctionArgument.uri;

  /*
    async function createWeb3Connection(playerId, chainId, uri) {
      const inventory = await openfort
      

    
    }
  */

    context.log("API call was successful and response sent.");
  } catch (error) {
    context.log("Unhandled error occurred:", error);
    context.res = {
      status: 500,
      body: JSON.stringify(error),
    };
  }
};

export default httpTrigger;
