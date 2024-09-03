const express = require('express');
const cors = require('cors');
const userRoutes = require('../routes/usuario.routes');
const app= express();

//middleware
app.use(express.json());
app.use(express.urlencoded({extended: true}));
app.use(cors());


//emdponint

app.use('/user',userRoutes);
app.get('/register', (req, res) => {
    res.sendFile(path.join(__dirname, 'views', 'register.html'));
});
app.get('/login', (req, res) => {
    res.sendFile(path.join(__dirname, 'views', 'loginpage.html'));
});
app.get('/dashboard', (req, res) => {
    res.sendFile(path.join(__dirname, 'views', 'dashboard.html'));
});
module.exports = app;