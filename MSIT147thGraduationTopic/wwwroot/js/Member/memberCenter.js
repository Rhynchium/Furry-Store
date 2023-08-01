﻿let memberData = null;
getMemberById();

//Ajax 得到會員資料
async function getMemberById(id) {
    let str = ''
    if (id) str = '/' + id

    const response = await fetch(`${ROOT}/api/apiMember${str}`);

    if (!response.ok) return;
    const data = await response.json();

    memberData = data
    console.log(data);
    displayMember();
}

//列出會員資料
function displayMember() {
    memberData.map((e) => {
        $('#nickName').val(e.nickName)
        $('#email').val(e.email)
        $('#phone').val(e.phone)
        $('#city').val(e.city)
        $('#district').val(e.district)
        $('#address').val(e.address)
    })
}

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

$('#nickNameEditedChk').click(function () {
    if ($('#nickNameEditedChk').is(':checked')) {
        $('#nickName').prop('disabled', false);
    } else {
        $('#nickName').prop('disabled', true);
    }
});

$('#passwordEditedChk').click(function () {
    if ($('#passwordEditedChk').is(':checked')) {
        $('#password').prop('disabled', false);
        $('#confirmPassword').prop('disabled', false);
    } else {
        $('#password').prop('disabled', true);
        $('#confirmPassword').prop('disabled', true);
    }
});

$('#emailEditedChk').click(function () {
    if ($('#emailEditedChk').is(':checked')) {
        $('#email').prop('disabled', false);
    } else {
        $('#email').prop('disabled', true);
    }
});

$('#phoneEditedChk').click(function () {
    if ($('#phoneEditedChk').is(':checked')) {
        $('#phone').prop('disabled', false);
    } else {
        $('#phone').prop('disabled', true);
    }
});

$('#addressEditedChk').click(function () {
    if ($('#addressEditedChk').is(':checked')) {
        $('#city').prop('disabled', false);
        $('#district').prop('disabled', false);
        $('#address').prop('disabled', false);
    } else {
        $('#city').prop('disabled', true);
        $('#district').prop('disabled', true);
        $('#address').prop('disabled', true);
    }
});

const myValid = new MyBootsrapValidator(document.querySelector('.needs-validation'))

function editValidator() {
    myValid.validateFunction(() => {
        const password = document.querySelector('#password')
        const confirmPassword = document.querySelector('#confirmPassword')
        const phone = document.querySelector('#phone')
        const email = document.querySelector('#email')

        let passwordValid = false
        const passwordHasValue = !!password.value
        const passwordPatternValid = /^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9]).{6,32}$/.test(password.value)
        const confirmPasswordValid = confirmPassword.value === password.value

        if ($('#passwordEditedChk').attr('checked')) {
            password.setValidate(() => passwordHasValue, '請輸入密碼')
                .setValidate(() => passwordPatternValid, '密碼格式錯誤')
                .setValidate(() => confirmPasswordValid, '與密碼不符')
            passwordValid = passwordHasValue && passwordPatternValid && confirmPasswordValid
        }
        else {
            password.setValidate(() => true)
            passwordValid = true
        }

        const phoneHasValue = !!phone.value
        const phonePatternValid = /^09\d{8}$/.test(phone.value)

        if ($('#phoneEditedChk').attr('checked')) {
            phone.setValidate(() => phoneHasValue, '請輸入手機號碼')
                .setValidate(() => phonePatternValid, '手機號碼格式錯誤')
        } else {
            phone.setValidate(() => true)
            phonePatternValid = true
        }        

        const emailHasValue = !!email.value
        const emailPatternValid = /^\w+@\w+/.test(email.value)

        if ($('#emailEditedChk').attr('checked')) {
            email.setValidate(() => emailHasValue, '請輸入Email')
                .setValidate(() => emailPatternValid, 'Email格式錯誤')
        } else {
            email.setValidate(() => true)
            emailPatternValid = true
        }        

        return passwordValid && phoneHasValue && phonePatternValid
            && emailHasValue && emailPatternValid
    })
    return myValid.startValidate()
}

function clearAllValidFeedBack() {
    //// 5. myValid.endtValidate() //結束驗證，順便清除顯示錯誤(如果送出表單後頁面會跳走，這個函式就用不到了)
    myValid.endtValidate()
}

$('#btnSubmit').click(editValidator)

const forms = document.querySelectorAll('.needs-validation')

Array.from(forms).forEach(form => {
    form.addEventListener('submit', async event => {
        event.preventDefault()
        event.stopPropagation()

        await Swal.fire({
            icon: 'question',
            title: '確定要修改嗎?',
            showCancelButton: true,
            allowOutsideClick: false,
        })

        if (!form.checkValidity()) {
            await Swal.fire({
                icon: 'error',
                title: '修改失敗!',
                text: '資料有錯誤,請修改',
                allowOutsideClick: false,
            })            
            return
        }

        const formData = new FormData(form)

        const response = await fetch(`${ROOT} /api/ApiMember/memberCenter`, {
            body: formData,
            method: 'put'
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
            title: '修改成功!',
            allowOutsideClick: false
        })

        window.location.reload();

    }, false)
})