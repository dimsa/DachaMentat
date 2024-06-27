FROM node:22-slim as build
WORKDIR /app
RUN rm -d /app
COPY package*.json ./
RUN npm install
COPY . .
RUN npm install -g @angular/cli
RUN ng build --configuration "production"

#STAGE 2
#COPY --from=build /usr/src/app/dist/sample-app /usr/share/nginx/html

#EXPOSE 4200
# Install http-server globally
#RUN npm install -g http-server
# Start the Angular app using http-server
#CMD ["http-server", "dist", "--port", "4200"]
#CMD ["ng", "serve", "--host", "0.0.0.0", "--port", "4200", "--disable-host-check"]
#RUN dir
#COPY ["/app/dist/", "mentang-app"]
#CMD ["tail", "-f", "/dev/null"]