# RateLimitApp
This .net console application written to determine the threshold level of the github rate limit for a given github PAT. If the threshold of remaining limit has come down below 10% of the allowed limit program will return with exit code 1 and 0 for the threshold anything above. Console application provided with docker file to create docker image which can be used to run the standalone application.

## Instructions for Developer
1. Clone Application from github repository <br /> *git clone https://github.com/NandunWithanachchi/RateLimitApp.git*
2. Change directory to RateLimitApp <br /> *cd RateLimitApp*
3. Checkout development branch <br /> *git checkout Dev-1*
4. Build docker image rate-limit-app <br /> *docker build -t rate-limit-app .*
5. Docker login <br />  *docker login -u User -p Password Optional:Repository*
6. Tag docker image <br /> *docker tag rate-limit-app Repository/rate-limit-app*
7. Push/Upload docker image <br /> *docker push Repository/rate-limit-app*

## Run/Test Application
### Prerequisites
Docker desktop should be installed <br />
Possess GitHub PAT/s 
### Instructions
1. Docker login <br /> *docker login -u User -p Password Optional:Repository*
2. Pull docker image <br /> *docker pull Repository/rate-limit-app*
3. Run docker image <br /> *docker run -it Repository/rate-limit-app rate-limit-app*
4. Enter the GitHub PAT 
