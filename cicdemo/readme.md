# Configure GitHub workflow

Perform login on Azure

```powershell
az login
```

## Create credentials for Azure authentication

In the GitHub workflow, you need to supply Azure credentials to authenticate to the Azure CLI. The following example creates a service principal with the Contributor role scoped to the resource group for your container registry.

First, get the resource ID of your resource group. Substitute the name of your group in the following az group show command:

```powershell
groupId=$(az group show \
  --name <resource-group-name> \
  --query id --output tsv)
```

Use az ad sp create-for-rbac to create the service principal:

```powershell
az ad sp create-for-rbac \
  --scope $groupId \
  --role Contributor \
  --sdk-auth
```

Output is similar to:

```json
{
  "clientId": "xxxx6ddc-xxxx-xxxx-xxx-ef78a99dxxxx",
  "clientSecret": "xxxx79dc-xxxx-xxxx-xxxx-aaaaaec5xxxx",
  "subscriptionId": "aaaa0a0a-bb1b-cc2c-dd3d-eeeeee4e4e4e",
  "tenantId": "aaaabbbb-0000-cccc-1111-dddd2222eeee",
  "activeDirectoryEndpointUrl": "https://login.microsoftonline.com",
  "resourceManagerEndpointUrl": "https://management.azure.com/",
  "activeDirectoryGraphResourceId": "https://graph.windows.net/",
  "sqlManagementEndpointUrl": "https://management.core.windows.net:8443/",
  "galleryEndpointUrl": "https://gallery.azure.com/",
  "managementEndpointUrl": "https://management.core.windows.net/"
}
```

Save the JSON output because it's used in a later step. Also, take note of the clientId, which you need to update the service principal in the next section.

## Update for registry authentication

Update the Azure service principal credentials to allow push and pull access to your container registry. This step enables the GitHub workflow to use the service principal to authenticate with your container registry and to push and pull a Docker image.

Get the resource ID of your container registry. Substitute the name of your registry in the following az acr show command:

```powershell
registryId=$(az acr show \
  --name <registry-name> \
  --resource-group <resource-group-name> \
  --query id --output tsv)
```

Use az role assignment create to assign the AcrPush role, which gives push and pull access to the registry. Substitute the client ID of your service principal (the clientId in the json above):

```powershell
az role assignment create \
  --assignee <ClientId> \
  --scope $registryId \
  --role AcrPush
```

## Save credentials to GitHub repo

In the GitHub UI, navigate to your forked repository and select Security > Secrets and variables > Actions.

Select New repository secret to add the following secrets:

| Secret                | Value                                                                                  |
| --------------------- | -------------------------------------------------------------------------------------- |
| AZURE_CREDENTIALS     | The entire JSON output from the service principal creation step                        |
| REGISTRY_LOGIN_SERVER | The login server name of your registry (all lowercase). Example: myregistry.azurecr.io |
| REGISTRY_USERNAME     | The clientId from the JSON output from the service principal creation                  |
| REGISTRY_PASSWORD     | The clientSecret from the JSON output from the service principal creation              |
| RESOURCE_GROUP        | The name of the resource group you used to scope the service principal                 |

Good luck
