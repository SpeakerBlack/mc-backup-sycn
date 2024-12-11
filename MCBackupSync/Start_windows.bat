@echo off

SET "server_jar=paper.jar"
SET "file_extension=zip"
SET "prefix_name=backup_paper_worlds"

java -Xms4G -Xmx6G -XX:+UseG1GC -XX:+UnlockExperimentalVMOptions -XX:MaxGCPauseMillis=50 -XX:+ParallelRefProcEnabled -XX:+DisableExplicitGC -Dfile.encoding=UTF-8 -jar "%server_jar%" nogui

FOR /F "tokens=2 delims==" %%I IN ('wmic os get localdatetime /value') DO SET datetime=%%I
SET year=%datetime:~0,4%
SET month=%datetime:~4,2%
SET day=%datetime:~6,2%
SET hour=%datetime:~8,2%
SET minute=%datetime:~10,2%
SET formatted_date=%year%_%month%_%day%_%hour%_%minute%
SET file_name=%prefix_name%_%formatted_date%.%file_extension%

7z a -tzip "%file_name%" "world" "world_nether" "world_the_end" -mmt=2

mcbackupsync "%file_name%"

DEL "%file_name%"