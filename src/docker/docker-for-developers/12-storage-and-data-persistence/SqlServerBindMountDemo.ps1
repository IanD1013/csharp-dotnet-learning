# Lesson 4: Mounting bind mounts in containers.
#
# Deviation from the course script: -d, so the script returns instead of holding
# the terminal on nginx's foreground log. Follow it with `docker logs -f nginx-withvol`
# if you want the log.
#
# Delete html/index.html before running to see the 403 the lesson describes;
# recreate it and refresh - no rebuild, no restart.

docker rm -f nginx-withvol

docker run `
  --name nginx-withvol `
  -p 1234:80 `
  -d `
  -v ${pwd}/html:/usr/share/nginx/html `
  nginx

"Serving ${pwd}\html on http://localhost:1234"
