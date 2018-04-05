
This is sample ARM template to deploye the app service plan based function app .
Orignal template from :  https://github.com/Azure/azure-quickstart-templates/tree/master/101-function-app-create-dedicated

This has been modified to pick the app setting part based on runtime and having multiple appsetting in it while remainng part is common for all function apps.

How to use:
Run following in powershell 
______________________________________
#Login-AzureRmAccount

New-AzureRmResourceGroupDeployment -ResourceGroupName "FunctionApps" -TemplateFile "C:\Users\papatida\Desktop\FuncARM\azuredeploy.json" -TemplateParameterFile "C:\Users\papatida\Desktop\FuncARM\azuredeploy.parameters.json" -DeploymentDebugLogLevel All -Debug
______________________________________

