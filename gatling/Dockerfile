# Use uma imagem base do OpenJDK 8
FROM openjdk:8

# Defina o diretório de trabalho no contêiner
WORKDIR /usr/src/app

# Copiando script run test
COPY ./run-test .

# Variável de ambiente para a versão do Gatling
ENV GATLING_VERSION="3.9.5"

# Crie um diretório para os downloads
RUN mkdir -p /usr/src/app/deps

# Baixe e instale o Gatling
RUN echo "Downloading Gatling ${GATLING_VERSION}" && \
    curl -fsSL "https://repo1.maven.org/maven2/io/gatling/highcharts/gatling-charts-highcharts-bundle/${GATLING_VERSION}/gatling-charts-highcharts-bundle-${GATLING_VERSION}-bundle.zip" > /usr/src/app/deps/gatling.zip && \
    cd /usr/src/app/deps/ && \
    unzip gatling.zip && \
    mv gatling-charts-highcharts-bundle-${GATLING_VERSION} gatling && \
    rm gatling.zip && \
    cd /usr/src/app && \
    echo "All done!"

# Defina a variável de ambiente GATLING_HOME
ENV GATLING_HOME /usr/src/app/deps/gatling

# Adicione o diretório bin do Gatling ao PATH
ENV PATH ${GATLING_HOME}/bin:${PATH}

# Exponha a porta 8000 para acessar os resultados do Gatling via web
EXPOSE 8000

# Adicione o diretório bin do Gatling ao PATH (pode ser necessário novamente após o WORKDIR)
ENV PATH ${GATLING_HOME}/bin:${PATH}

# Copiando a user-files
COPY ./user-files /usr/src/app/deps/gatling/user-files

# Defina o ponto de entrada do contêiner
ENTRYPOINT ["bash"]