FROM ubuntu:trusty

ENV DEBIAN_FRONTEND=noninteractive
ENV DOTNET_SKIP_FIRST_TIME_EXPERIENCE=1
ENV DOTNET_CLI_TELEMETRY_OPTOUT=1

RUN apt-get update \
 && apt-get install -y curl \
 && curl https://packages.microsoft.com/keys/microsoft.asc | gpg --dearmor > microsoft.gpg \
 && mv microsoft.gpg /etc/apt/trusted.gpg.d/microsoft.gpg \
 && apt-key adv --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys 3FA7E0328081BFF6A14DA29AA6A19B38D3D831EF \
 && echo "deb https://download.mono-project.com/repo/ubuntu stable-trusty main" | tee /etc/apt/sources.list.d/mono-official-stable.list \
 && echo "deb [arch=amd64] https://packages.microsoft.com/repos/microsoft-ubuntu-trusty-prod trusty main" | tee /etc/apt/sources.list.d/dotnetdev.list \
 && apt-get install -y apt-transport-https \
 && apt-get update \
 && apt-get install -y \
        mono-devel \
        dotnet-sdk-2.1.2 \
        dotnet-hostfxr-1.0.1 \
        dotnet-sharedframework-microsoft.netcore.app-1.0.5 \
 && apt-get clean \
 && rm -rf /var/lib/apt/lists/* /tmp/*

CMD [ "bash" ]
