const app = require('./app');
const database = require('./database');

const main = () =>{
    database.connect((err)=>{
        if(err) throw err;
        console.log('bd conectada');
    });

    app.listen(3000, () => {
        console.log('servidor escuchando');
    });
};

main();