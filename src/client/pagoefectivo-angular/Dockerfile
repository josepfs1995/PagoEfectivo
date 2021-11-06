FROM node:14 as ng-builder
RUN mkdir -p /app

WORKDIR /app
COPY package.json /app
RUN npm install
COPY . /app
RUN $(npm bin)/ng build --prod

FROM nginx
COPY nginx.conf /etc/nginx/nginx.config
COPY --from=ng-builder /app/dist/pagoefectivo-angular /usr/share/nginx/html

EXPOSE 80
