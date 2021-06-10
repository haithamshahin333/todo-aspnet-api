# Lab 1 - Deploy Azure Resource for Application Insights

## Create Log Analytics Workspace

References:
- [Create a Log Analytics Workspace](https://docs.microsoft.com/en-us/azure/azure-monitor/logs/quick-create-workspace)
- [Example Templates for Insights Deployments](https://github.com/Azure/azure-quickstart-templates/tree/master/quickstarts/microsoft.insights)
- [Azure Monitor Logs Overview](https://docs.microsoft.com/en-us/azure/azure-monitor/logs/data-platform-logs)
- [Manage usage and costs](https://docs.microsoft.com/en-us/azure/azure-monitor/logs/manage-cost-storage#changing-pricing-tier)
- [Azure Monitor Pricing](https://azure.microsoft.com/en-us/pricing/details/monitor/)

### Overview:

Data collected by [Azure Monitor Logs]((https://docs.microsoft.com/en-us/azure/azure-monitor/logs/data-platform-logs)) is stored in Log Analytics Workspaces, including log data from Application Insights when creating a workspace-based application.

In this lab, we will create the Log Analytics Workspace needed to support our Application Insights deployment.

### Steps:

1. Create a Resource Group to be used for this exercise in the portal by searching for 'Resource Groups' in the Azure Portal.

2. Create a Resource Group named 'app-insights-testing' and click 'Create':

![Resource Group](./assets/resource-group.png)

3. From here, follow the [tutorial](https://docs.microsoft.com/en-us/azure/azure-monitor/logs/quick-create-workspace) and create the log analytics workspace in the 'app-insights-testing' resource group.

4. Once complete, confirm that the Deployment succeeded and you should be able to select the resource and see the Log Analytics workspace overview tab.

## Create a Workspace-based Application Insights Resource

### Overview:

[Workspace-based Application Insights](https://docs.microsoft.com/en-us/azure/azure-monitor/app/create-workspace-resource) is the latest deployment model for Application Insights.

### Steps:

1. Similar to the steps above, follow the [tutorial](https://docs.microsoft.com/en-us/azure/azure-monitor/app/create-workspace-resource) to create an Application Insights resource within your new resource group.

> Info: Be sure that the Resource Mode is set to Workspace-based.

> Info: Be sure that you select the new Log Analytics workspace resource when going through the App Insights setup.

![App Insights Creation](./assets/app-insights.png)

2. Once complete, confirm the Deployment succeeded and you should be able to select the resource and see the overview tab (empty metrics at this point since we haven't added app insights to our app').

# Lab 2 - Create App Service for Todo App

1. Navigate to the 'app-insights-testing' resource group where the Log Analytics workspace and App Insights resources were created.

2. Select 'Create' and then select 'Web App':

![Web App Create](./assets/web-app-create.png)

3. Work through the portal and make selections as shown below. Note that the name of the app will need to be unique. Be sure to select `.NET 5` for the Runtime stack:

![Web App Form](./assets/web-app-form.png)

4. On the Deployment section, select 'Disable'.

5. On the Monitoring section, select 'No' to Application Insights since we will instrument this with the SDK. If both were selected, the manual/sdk insights would only be respected as described [here](https://docs.microsoft.com/en-us/azure/azure-monitor/app/azure-web-apps?tabs=net#enable-application-insights).

6. Select 'Create'. Once the Deployment succeeds, confirm that you can select the resource and see the following page when you select your app url:

![Web App Validation](./assets/app-service-deployment-confirmation.png)

# Lab 3 - Add Application Insights to the Application

## Add Application Insights SDK

### Overview:

Depending on your IDE, there may be different methods of how to enable Application Insights. [Here are the steps for ASP.NET Core Apps](https://docs.microsoft.com/en-us/azure/azure-monitor/app/asp-net-core).

### Steps:

1. Run through this [tutorial](https://docs.microsoft.com/en-us/azure/azure-monitor/app/asp-net-core) to add the Application Insights SDK to your app.

2. Run the app locally from your IDE and view Live Metrics to confirm things are working as expected.





    Live Metrics View

    Overview Dashboard -> Drill Down

    Metric Definition: https://docs.microsoft.com/en-us/azure/azure-monitor/app/standard-metrics
        show metric defn and how it changes by deployment platform

# Lab 3 - Deploy App to App Service