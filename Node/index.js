const express = require('express')
const fs = require('fs');
const playerRoutes = require('./Routes/PlayerRoutes');
const app = express();
const port = 4000;

app.use(express.json);
app.use('/api',playerRoutes);
const resourceFilePath = 'resources.json';

loadResource();     //서버 시작 시 자원 로드

function loadResource()         
{
    if(fs.existsSync(resourceFilePath))     //파일 경로를 확인해서 파일이 있는지 확인
    {
        const data = fs.readFileSync(resourceFilePath)
        global.players = JSON.parse(data);              //파일에서 로딩
    }
    else{
        global.players = {};        //초기화
    }
}


app.listen(port, () => 
{
    console.log(`서버가 http://localhost:${port}에서 실행 중 입니다.`);
})
