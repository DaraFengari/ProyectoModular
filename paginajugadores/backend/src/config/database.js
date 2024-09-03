const myql2 = require('mysql2');

// Database
const connection = myql2.createConnection({
    host: 'localhost',
    user: 'root',
    password: 'Xcvbnjk1@',
    database: 'jugadores',

});

module.exports = connection;