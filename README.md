# HTML Parser

## Installation 

### Pre-requisites 
You’ll need the following development tools installed:
* .NET Core 3.1 SDK latest edition (https://dotnet.microsoft.com/download/dotnet-core/thank-you/sdk-3.1.406-windows-x64-installer);
* Node.js latest stable version (https://nodejs.org/en/). I am currently use 14.15.5;
* Yarn package manager should be installed globally. Open *Window Powershell in Admin mode* and run the following command: `npm install -g yarn`.

### Project setup

* Clone the `master` branch of the current repository into the directory of choice.
* Open *Window Powershell in Admin mode* and navigate to the currently downloaded repository: `cd {path-to-the-directory-of-choice}\react-htmlparser\`
* Run `yarn install` to install all project dependencies.  
* Run `yarn run dev` to run server and client services. At the beginning of the execution it may ask you to trust 'localhost' certificate, choose 'yes'. Finally it should look like below:
![Run](https://user-images.githubusercontent.com/1925984/107998940-3dd66c80-6ff7-11eb-91fb-eb2e645b9164.png)

Where, https://localhost:5001 - is a server side host (Swagger is available by the link: https://localhost:5001/swagger/index.html),
http://localhost:3000/ - is a client side React JS app:

![React JS App](https://user-images.githubusercontent.com/1925984/107999129-c6eda380-6ff7-11eb-95d6-7381439fda80.png)
