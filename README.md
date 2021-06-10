# Lab 1 - Deploy Azure Resource for Application Insights

## Create Log Analytics Workspace

References:
- [Create a Log Analytics Workspace](https://docs.microsoft.com/en-us/azure/azure-monitor/logs/quick-create-workspace)
- [Example Templates for Insights Deployments](https://github.com/Azure/azure-quickstart-templates/tree/master/quickstarts/microsoft.insights)
- [Azure Monitor Logs Overview](https://docs.microsoft.com/en-us/azure/azure-monitor/logs/data-platform-logs)
- [Manage usage and costs](https://docs.microsoft.com/en-us/azure/azure-monitor/logs/manage-cost-storage#changing-pricing-tier)
- [Azure Monitor Pricing](https://azure.microsoft.com/en-us/pricing/details/monitor/)
- [Metric Definition](https://docs.microsoft.com/en-us/azure/azure-monitor/app/standard-metrics)

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

2. If configuring from your IDE, be sure that the connection string to app insights is not committed to a public repo!

3. Add the app insights connection string to your appsettings.Development.json file so you can test running it locally:

```
  "ApplicationInsights": {
    "ConnectionString": ""
  }
```

4. To add app insights to our client-side code, follow this [snippet tutorial for JavaScript](https://docs.microsoft.com/en-us/azure/azure-monitor/app/javascript#snippet-based-setup).

5. To validate the setup, run the app locally and then navigate to Live Metrics to confirm that data is being streamed to Azure:

![Live Metrics](./assets/live-metrics.png)

6. Navigate to your App Service and under Configuration, add `APPLICATIONINSIGHTS_CONNECTION_STRING` as a new application setting. The value will be your Connection String:

![App Setting Config](./assets/app-setting-config.png)q

7. Deploy the App and you should now see metrics being streamed from your app running on App Services.

# Lab 4 - Understand Overview and Create Basic Dashboard

1. Navigate to your App Insights Resource and select the Overview tab.

2. Select into Failed Requests. You can drill down into the data by selecting on the different operations:

![Failed Requests](./assets/failed-requests.png)

3. Let's create a Failure Dashboard where we can continue to track this information. On the Portal Home Screen, select the pop-out blade on the left and Select 'Dashboard':

![Dashboard Blade](./assets/dashboard-blade.png)

4. Select New Dashboard, Blank Dashboard. From there, title the Dashboard 'Failure Dashboard'.

5. Navigate back to your Failed Requests overview and select the pin - you can then select your new Failure Dashboard to pin that tile to the Dashboard.

6. Additional steps for adding [log data](https://docs.microsoft.com/en-us/azure/azure-monitor/visualize/tutorial-logs-dashboards) and [application data](https://docs.microsoft.com/en-us/azure/azure-monitor/app/tutorial-app-dashboards) to the dashboard.

# Lab 5 - Create an Alert

[Overview](https://docs.microsoft.com/en-us/azure/azure-monitor/alerts/alerts-log#overview)

1. First, navigate to Log Analytics and let's create a basic query that will return failed requests for a particular Operation we are interested in:

```
requests
| where success == false and operation_Name has "GET /failure"
```

2. Once the query is created, we can select New Alert Rule directly from the query editor:

![Query Create Alert](./assets/create-alert-rule.png)

3. In the Alert view, select the condition and configure the signal as shown below:

![Signal Logic](./assets/signal-logic.png)

4. In a separate tab before continuing with creating that Alert, open up the Azure Portal and go to create an Action Group by Searching for Alerts. From there when on the page for Alerts, select Manage Actions:

![Manage Actions](./assets/manage-actions.png)

5. Go through the form for adding a [new action group](https://docs.microsoft.com/en-us/azure/azure-monitor/alerts/action-groups). No actions are required (you can setup an email alert for this scenario).

6. Once complete, go back to the alert rule creation form and select that action group.

7. You can add an email subject and description and enable the rule upon creation.

8. Go ahead and trigger an alert by hitting your app at the '/failure' endpoint.

9. Finally, navigate back to the Log Analytics query used for the alert and add it to your Failure Dashboard.

# Lab 6 - Create an Availability Test

[Overview](https://docs.microsoft.com/en-us/azure/azure-monitor/app/monitor-web-app-availability)

1. Navigate to your Application Insights resource.

2. Select the Availability tab which won't have data at this point.

3. Click on 'Add a Test' and setup a test as shown below:

![Availability Test](./assets/availability-test-creation.png)

> Info: This is a [tutorial](https://docs.microsoft.com/en-us/azure/azure-monitor/app/monitor-web-app-availability) going through a similar exercise as reference.

4. Once the tests are up and running, you should begin to see data stream in the graph:

![Availability Test Success](./assets/availability-test-success.png)

# Lab 7 - User Behavior

[Overview](https://docs.microsoft.com/en-us/azure/azure-monitor/app/usage-overview)

By having the application insights snippet added to our JavaScript, we can monitor and learn usage behavior.

1. Navigate to your application insights resource.

2. Scroll down to the Usage blade on the left - you will find there the metrics gathered for your usage behavior insights.

3. Select Users as shown below and then View More Insights - you should see a screen similar to what is below:

![Usage Behavior](./assets/usage-behavior.png)

4. To view how users are being funneled through your app, select Funnels under Usage

5. Create a funnel like what is shown below:

![Funnel](./assets/funnel.png)

6. Hit the View tab to see the results and how users are being retained as they work through the site
