FROM node:16-alpine3.15 as builder
WORKDIR /app

ENV PATH /app/node_modules/.bin:$PATH
ENV REACT_APP_USE_LOCAL_CONFIG=false
COPY . .

RUN dos2unix /app/docker/entrypoint.sh /app/docker/entrypoint.sh
RUN chmod +x /app/docker/entrypoint.sh

RUN yarn --frozen-lockfile
RUN yarn build --configuration=production

COPY . /app

FROM nginx:mainline-alpine

RUN apk add --no-cache jq

RUN rm /etc/nginx/conf.d/default.conf
COPY --from=builder /app/docker/nginx.conf /etc/nginx/conf.d
COPY --from=builder /app/docker/entrypoint.sh /entrypoint.sh
COPY --from=builder /app/build/ /usr/share/nginx/html/

RUN ["chmod", "+x", "/entrypoint.sh"]
ENV CONFIG_JSON_PATH=/usr/share/nginx/html/config.json

ENTRYPOINT ["sh", "/entrypoint.sh"]
