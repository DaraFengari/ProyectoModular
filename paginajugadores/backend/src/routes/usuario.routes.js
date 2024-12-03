const { Router } = require('express');

const {createuser,readuser,updateuser,deleteuser, loginUser,guardarPuntuacion, getGlobalScores, readscore} = require('../controllers/user.controller');
const { route } = require('../config/app');

const router = Router();

router.get('/:id', readuser);

router.post('/',createuser);

router.put('/:id',updateuser);

router.delete('/:id',deleteuser);

router.post('/login', loginUser);

router.post('/puntuaciones', guardarPuntuacion);

router.get('/escoreglobal/:id',getGlobalScores);

router.get('/score/:id',readscore);


module.exports = router;