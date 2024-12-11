#!/bin/bash

server_jar="paper.jar"
file_extension="zip"
prefix_name="backup_paper_worlds"

java -Xms4G -Xmx6G -XX:+UseG1GC -XX:+UnlockExperimentalVMOptions -XX:MaxGCPauseMillis=50 -XX:+ParallelRefProcEnabled -XX:+DisableExplicitGC -Dfile.encoding=UTF-8 -jar "$server_jar" nogui

formatted_date=$(date +"%Y_%m_%d_%H_%M")

output_file="${prefix_name}_${formatted_date}.${file_extension}"

7z a -tzip "$output_file" "world" "world_nether" "world_the_end" -mmt=2

./mcbackupsync "$output_file"

rm "$output_file"