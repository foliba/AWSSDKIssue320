#!/bin/bash

echo "" >out.txt
for i in {1..5000}
do
  ts=0
  ts=$(date +%H:%M:%S:%N)
  echo $ts
  time curl -X POST -H "Content-Type: application/json" -d "{\"SomeId\":\"fakeId_${i}_${ts}\"}" localhost:16616/account/login >> out.txt &
  echo ""
 done
echo "done"
