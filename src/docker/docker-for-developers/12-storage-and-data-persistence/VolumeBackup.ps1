# Lesson 6: Backing up volumes.
#
# Deviations from the course script:
#   - RestoredVolume is removed up front too, so the script is re-runnable.
#   - the write step drops -it and adds --rm: nothing reads stdin, and -it fails
#     outright in a non-interactive shell.

"Delete volumes if they already exist..."
docker volume rm BackupDemoVolume
docker volume rm RestoredVolume

"Create volume..."
docker volume create BackupDemoVolume

"Write file to our volume..."
docker run `
  --rm `
  -v BackupDemoVolume:/mydata `
  alpine `
  sh -c "echo 'Hello' > /mydata/hello.txt"

"Create backup of volume..."
docker run `
  --rm `
  -v BackupDemoVolume:/mydata `
  -v ${pwd}:/backup `
  alpine `
  sh -c "cd /mydata && tar cvf /backup/backup.tar *"

"Restore backup..."
docker run `
  --rm `
  -v RestoredVolume:/mydata `
  -v ${pwd}:/backup alpine sh `
  -c "cd /mydata && tar xvf /backup/backup.tar"

"Reading hello.txt back out of RestoredVolume..."
docker run --rm -v RestoredVolume:/mydata alpine cat /mydata/hello.txt
