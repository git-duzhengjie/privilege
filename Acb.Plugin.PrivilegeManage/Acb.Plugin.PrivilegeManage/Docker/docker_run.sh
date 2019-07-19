#!/bin/bash
echo "=======================rm&&rmi docker========================="
(docker rm -f privilege-manage&&docker rmi -f icb/privilege-manage:$1)|| echo "continue execute"
echo "=======================docker build==========================="
docker build -t icb/privilege-manage:$1  .
