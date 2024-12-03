const express = require('express');
const cors = require('cors');
const userRoutes = require('../routes/usuario.routes');
const app= express();

//middleware
app.use(express.json());
app.use(express.urlencoded({extended: true}));
app.use(cors());
app.use(cors({
    origin: '*', // Permitir todas las conexiones, solo para pruebas
    methods: ['GET', 'POST'],
}));
  

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
app.get('/escoreglobal', (req, res) => {
    res.sendFile(path.join(__dirname, 'views', 'globalscore.html'));
});
app.get('/myprofile', (req, res) => {
    res.sendFile(path.join(__dirname, 'views', 'myprofile.html'));
});
module.exports = app;