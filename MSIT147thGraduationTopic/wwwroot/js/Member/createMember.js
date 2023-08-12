﻿const today = new Date().toISOString().split('T')[0];
$("#DateOfBirth").attr('max', today);

function checkPasswordMatch() {
    const password = $('#password').val();
    const confirmPassword = $('#confirmPassword').val();

    if (password !== confirmPassword) {
        $('#confirmPassword').addClass('is-invalid');
    } else {
        $('#confirmPassword').removeClass('is-invalid');
    }
}

$('#confirmPassword').blur(function () {
    checkPasswordMatch();
});

const selCity = document.querySelector('#city');
const selDistrict = document.querySelector('#district');

LoadCities();

async function LoadCities() {
    try {
        const response = await fetch('/Member/Cities');
        const datas = await response.json();

        var cities = datas.map(city => {
            return (`<option value="${city}">${city}</option>`);
        });

        selCity.innerHTML = cities.join("");
        LoadDistricts();
    } catch (error) {
        console.error(error);
    }
}

async function LoadDistricts() {
    try {
        const city = selCity.options[selCity.selectedIndex].value;
        const response = await fetch(`/Member/Districts?city=${city}`);
        const datas = await response.json();

        var districts = datas.map(district => {
            return (`<option value="${district}">${district}</option>`);
        });

        selDistrict.innerHTML = districts.join("");
    } catch (error) {
        console.error(error);
    }
}

selCity.addEventListener('change', () => {
    LoadDistricts();
});

$("#demoMemberRegister").click(() => {
    $('#memberName').val('王石油');
    $("input[name=Gender][value='male']").attr('checked', true);
    $('#nickName').val('VVVIP');
    $('#account').val('demoMember99');
    $('#password').val('demoMember99');
    $('#confirmPassword').val('demoMember99');
    $('#email').val('demoMember999@gmail.com');
    $('#phone').val('0912345678');
    $('#city').val('臺北市');
    $('#district').val('大安區');
    $('#address').val('復興南路一段390號2樓');
    $('#DateOfBirth').val('1999-09-09');
})

const myValid = new MyBootsrapValidator(document.querySelector('.needs-validation'))

function createValidator() {
    
    myValid.validateFunction(() => {
        const name = document.querySelector('#memberName')
        const nameHasValue = Boolean(name.value)
        
        name.setValidate(() => nameHasValue, '請輸入姓名')

        const account = document.querySelector('#account')
        const accountHasValue = !!account.value
        account.setValidate(() => accountHasValue, '請輸入帳號')

        const password = document.querySelector('#password')
        const confirmPassword = document.querySelector('#confirmPassword')
        const passwordHasValue = !!password.value
        const passwordPatternValid = /^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9]).{6,32}$/.test(password.value)
        const confirmPasswordValid = confirmPassword.value === password.value
        password.setValidate(() => passwordHasValue, '請輸入密碼')
                .setValidate(() => passwordPatternValid, '密碼格式錯誤')
                .setValidate(() => confirmPasswordValid, '與密碼不符')
        passwordValid = passwordHasValue && passwordPatternValid && confirmPasswordValid

        const phone = document.querySelector('#phone')
        const phoneHasValue = !!phone.value
        const phonePatternValid = /^09\d{8}$/.test(phone.value)
        phone.setValidate(() => phoneHasValue, '請輸入手機號碼')
             .setValidate(() => phonePatternValid, '手機號碼格式錯誤')

        const email = document.querySelector('#email')
        const emailHasValue = !!email.value
        const emailPatternValid = /^\w+@\w+/.test(email.value)
        email.setValidate(() => emailHasValue, '請輸入Email')
             .setValidate(() => emailPatternValid, 'Email格式錯誤')
        
        return nameHasValue && phoneHasValue && phonePatternValid && emailHasValue && passwordValid
            && accountHasValue && emailPatternValid
    })    
    return myValid.startValidate()
}

$('#btnSubmit').click(createValidator)

const forms = document.querySelectorAll('.needs-validation');

Array.from(forms).forEach(form => {
    form.addEventListener('submit', async event => {
        event.preventDefault()
        event.stopPropagation()

        if (!form.checkValidity()) {
            await Swal.fire({
                icon: 'error',
                title: '註冊失敗!',
                text: '資料有錯誤,請修改',
                allowOutsideClick: false,
            })
            form.classList.add('was-validated')
            return
        }

        const formData = new FormData(form)

        const response = await fetch(`${ROOT} /api/ApiMember`, {
            body: formData,
            method: 'post'
        })

        if (!response.ok) {
            console.log('輸入失敗')
            return
        }

        const memberId = await response.json()

        if (memberId <= 0) {
            console.log('沒有成功寫入資料庫')
            return
        }

        await Swal.fire({
            icon: 'success',
            title: '註冊成功!',
            allowOutsideClick: false
        })

        window.location.href = `${ROOT} /home/index`

    }, false)
})

