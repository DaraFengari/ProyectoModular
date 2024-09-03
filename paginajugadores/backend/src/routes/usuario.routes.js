const { Router } = require('express');

const {createuser,readuser,updateuser,deleteuser, loginUser} = require('../controllers/user.controller');

const router = Router();

router.get('/:id', readuser);

router.post('/',createuser);

router.put('/:id',updateuser);

router.delete('/:id',deleteuser);

router.post('/login', loginUser);


module.exports = router;