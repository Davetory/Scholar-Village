var { MongoClient} = require('mongodb');
const AWS = require('aws-sdk')
var bcrypt = require('bcrypt');

AWS.config.update({accessKeyId: 'AKIA6D2QNGPKMH2YXJVQ', secretAccessKey: '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', signatureVersion: 'v4', region: 'us-east-2'})
const s3 = new AWS.S3()

var url = 'mongodb+srv://dbUser:tapAjOMxZ83NdOkA@cluster0.ie7my.mongodb.net/cps731?retryWrites=true&w=majority';

const nodemailer = require("nodemailer");
var handlebars = require('handlebars');
var fs = require('fs');

var db = null;
var client = null;
async function connect() {
    if (db == null) {
        var options = {
            useUnifiedTopology: true,
        };

    client = await MongoClient.connect(url, options);
    db = await client.db("cps731");
    }

    return db;
}

/*
async function register(firstName, lastName, email, studentID, username, password, confirm_password) {
    var conn = await connect();
    conn.collection('users').insertOne({ firstName, lastName, email, studentID, username, password, confirm_password });
}
*/

var readHTMLFile = function(path, callback) {
    fs.readFile(path, {encoding: 'utf-8'}, function (err, html) {
        if (err) {
            throw err;
            callback(err);
        }
        else {
            callback(null, html);
        }
    });
  };

var smtpTransport = nodemailer.createTransport({
    host: 'smtp.gmail.com',
    port: 465,
    secure: true, //secure : true for 465, secure: false for port 587 
    auth: {
        user: "yourscholarvil@gmail.com",
        pass: "Sv.123456",
    },
  });


async function sendEmail (email, code) {
    readHTMLFile(__dirname + '/routes/email.html', function(err, html) {
      var template = handlebars.compile(html);
      var replacements = {
           code: code
      }
      var htmlToSend = template(replacements);

      var mailOptions = {
        from: '"Scholar Village" <scholarvillage@ryerson.ca>', // sender address
        to: email, // receiver address
        subject: 'Please verify your Scholar Village account', // Subject line
        html: htmlToSend // text
      };
  
      // send mail with defined transport object
      smtpTransport.sendMail(mailOptions, (error, info) => {
        if (error) {
            return console.log(error);
        }
        console.log('Message %s sent: %s', info.messageId, info.response);
      });
    });
  };

const emailRegexp = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$/;
async function register(firstName, lastName, email, username, password, verified, code) {
    var conn = await connect();

    var existingUser = await conn.collection('users').findOne({ username });
    var existingEmail = await conn.collection('users').findOne({ email });
    console.log(password)
    console.log(existingUser, existingEmail, !emailRegexp.test(email), password.length < 6)
    if (existingUser != null) {
        throw new Error('User already existed!');
    }

    if (existingEmail != null) {
        throw new Error("The email is already used!");
    }

    if(!(emailRegexp.test(email))) {
        throw new Error("Invalid email address.");
    }

    if (password.length < 6) {
        throw new Error(" Your password is too short.");
    }

    var SALT_ROUNDS = 10;
    var passwordHash = await bcrypt.hash(password, SALT_ROUNDS);

    conn.collection('users').insertOne({ firstName, lastName, email, username, passwordHash, verified, code });
}

async function verifyAccount(email, inputCode) {
    var conn = await connect();
    var user = await conn.collection('users').findOne({ email });

    if (user == null) {
        throw new Error('The email cannot be found. Please check your credential again.');
    }

    var validCode = user.code;
    if (inputCode != validCode) {
        throw new Error('The code you entered is wrong. Please check your credential again.');
    }

    var verify = user.verified;
    if (verify == "True") {
        throw new Error('The account has been verified. Please login!');
    }

    await conn.collection("users").updateOne(user, { $set: { verified: "True"} });
    await conn.collection("users").updateOne(user, { $unset: { code: "" } });
}

async function resendCode(email) {
    

    if (user == null) {
        throw new Error('The email cannot be found. Please check your credential again.');
    }
    var conn = await connect();
    var user = await conn.collection('users').findOne({ email });

    var userCode = user.code;
    sendEmail(email, userCode);
}

async function login(username, password) {
    var conn = await connect();
    
    var user = await conn.collection('users').findOne({ username });
    if (user == null) {
        throw new Error('User does not exist! Please create an account.');
    }

    var valid = await bcrypt.compare(password, user.passwordHash)
    if (!valid) {
        throw new Error('Invalid Password! Please re-enter your password.');
    }

    var verified = user.verified;
    if (verified == "False") {
        throw new Error('Your account is not verified. Please verify your account.');
    }
}

async function login(username, password) {
    var conn = await connect();

    var user = await conn.collection('users').findOne({ username });
    if (user == null) {
        throw new Error('User does not exist! Please create an account.');
    }

    var valid = await bcrypt.compare(password, user.passwordHash)
    if (!valid) {
        throw new Error('Invalid Password!');
    }
    return user;
}

async function newConvo(members){

    const conn = await connect();   
    await conn.collection('convos').insertOne({members});
    
}

async function getConvo(userId){

    const conn = await connect();
    const convo = await conn.collection('convos').find({
        members: { $in: [userId] }}).toArray();
    return(convo);
}

async function singleConvo(){

}

async function sendMessage(msg){
    const conn = await connect();
    const insert = await conn.collection('messages').insertOne(msg);
    return insert;
}

async function getMessages(conversationId){
    const conn =  await connect();
    const messages = await conn.collection('messages').find({convoId: conversationId,}).toArray();
    return messages;
}

async function getAllUsers(){
    const conn = await connect();
    const users = await conn.collection('users').find().toArray();
    return users;
}

async function getOne(username){
    const conn = await connect();
    const user = await conn.collection('users').findOne({username: username});
    return user;
}
async function getOneWithId(id){

    const conn = await connect();
    const user = await conn.collection('users').findOne({_id: id});
    
    return user;
}
async function getSpecificConvo(first,second){
    const conn = await connect();
    const convo = await conn.collection('convos').findOne({members: { $all: [first, second] }});
    return convo;
}

async function modify(username, oldPassword, newPassword, confirmPassword) {
    var conn = await connect();
    var user = await conn.collection('users').findOne({ username });

    var valid = await bcrypt.compare(oldPassword, user.passwordHash)

    if (!valid) {
        throw new Error('Your old password is wrong!');
    }

    if (newPassword != confirmPassword) {
        throw new Error('Your confirmPassword not the same as your newPassword!.');
    }

    if (newPassword.length < 6) {
        throw new Error(" Your newPassword is too short.");
    }

    var SALT_ROUNDS = 10;
    var newPasswordHash = await bcrypt.hash(newPassword, SALT_ROUNDS);
    await conn.collection("users").updateOne(user, { $set: { passwordHash: newPasswordHash} });
}

async function getURL() {
    const myBucket = 'lecturecps731';
    const file = 'lecture1.mp4'
    var lecUrl =  s3.getSignedUrl('getObject', {
        Bucket: myBucket,
        Key: file,
        Expires: 15*60
    });
    return lecUrl;
/*
    const myBucket = 'lecturecps731';
    var lectureNo = 1;
    var interval;
    var startTime;
    var duration;
    var conn = await connect();
    async function pullLecInfo(lecID){
        var value = 'lecture' + lecID.toString();
        query = {};
        query["name"] = value;
        var lec = await conn.collection('lecture').findOne(query);
        startTime = lec.startTime;
        duration = lec.duration;

        return lec;
    };

    function pullLecURL(time, i) {
        const file = 'lecture' + i + '.mp4'
        var lecUrl =  s3.getSignedUrl('getObject', {
            Bucket: myBucket,
            Key: file,
            Expires: time*60
        });
        return lecUrl;
    };


    function computeTimeDiff(time1, time2){ //int time1, time2
        return ((parseInt(time1/100)*60 + (time1%100)) - time2)
    };
    function convertTime(time){ //string time
        return parseInt((parseInt(time/60)).toString() + (parseInt(time % 60)).toString())
    };

    await pullLecInfo(lectureNo);

    async function loopLectureBase (callback) { 
        var lectureUrl;
        var now = new Date()
        var currentTime = now.getHours()*60 + now.getMinutes()
        let diff = computeTimeDiff(parseInt(startTime) + convertTime(parseInt(duration)), currentTime);
        if (diff < 0) {
            lectureNo += 1;
            let y = await pullLecInfo(lectureNo);
            if (y == null) {
                clearInterval(interval);
            }
            else {
                now = new Date();
                currentTime = now.getHours()*60 + now.getMinutes()
                diff = computeTimeDiff(parseInt(startTime) + convertTime(parseInt(duration)), currentTime);
                lectureUrl = pullLecURL(Math.abs(diff), lectureNo);
                clearInterval(interval);
                interval = setInterval(loopLectureBase, Math.abs(diff*1000*60));
            }
        }
        else {
            lectureUrl = pullLecURL(Math.abs(diff), lectureNo);
            clearInterval(interval)
            interval = setInterval(loopLectureBase, Math.abs(diff*1000*60));
        }
        callback(lectureUrl);
    }

    var final = loopLectureBase(function(result) {
        return result;
    });
*/
}; 

module.exports = {
    login,
    register,
    verifyAccount,
    sendEmail,
    resendCode,
    modify,
    newConvo,
    getConvo,
    singleConvo,
    sendMessage,
    getMessages,
    getAllUsers,
    getOne,
    getOneWithId,
    getSpecificConvo,
    getURL,
    url,
}