// Function to validate form fields
function validateForm() {
    let isValid = true;
    let errorMessage = '';

    // Get field values
    const fname = document.getElementById('fname').value.trim();
    const lname = document.getElementById('Lname').value.trim();
    const email = document.getElementById('email').value.trim();
    const contact = document.getElementById('contact').value.trim();
    const referenceVideo = document.getElementById('feference-video').value.trim();
    const serviceRequirement = document.getElementById('Service-Requirement').value.trim();

    // Regular expressions
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    const phoneRegex = /^[0-9]{10}$/;

    // Validate First Name
    if (!fname) {
        isValid = false;
        errorMessage += 'First Name is required.<br>';
    }

    // Validate Last Name
    if (!lname) {
        isValid = false;
        errorMessage += 'Last Name is required.<br>';
    }

    // Validate Email
    if (!email || !emailRegex.test(email)) {
        isValid = false;
        errorMessage += 'A valid Email is required.<br>';
    }

    // Validate Contact Number
    if (contact && !phoneRegex.test(contact)) {
        isValid = false;
        errorMessage += 'Contact must be a 10-digit number.<br>';
    }

    // Validate Reference Video
    if (!referenceVideo) {
        isValid = false;
        errorMessage += 'Reference Video is required.<br>';
    }

    // Validate Service Requirement
    if (!serviceRequirement) {
        isValid = false;
        errorMessage += 'Service Requirement is required.<br>';
    }

    return { isValid, errorMessage };
}

// Function to handle form submission
function handleFormSubmission(event) {
    event.preventDefault(); // Prevent default form submission

    const { isValid, errorMessage } = validateForm();

    if (!isValid) {
        // Show warning modal with validation messages
        document.getElementById('validation-errors').innerHTML = errorMessage;
        $('#warning-message').modal('show');
        return;
    }

    // Simulate form data to send via AJAX (replace with actual form logic)
    const emailData = {
        toEmail: document.getElementById('email').value,
        subject: 'Enquiry Form Submission',
        body: `First Name: ${document.getElementById('fname').value}\nLast Name: ${document.getElementById('Lname').value}\nReference Video: ${document.getElementById('feference-video').value}\nService Requirement: ${document.getElementById('Service-Requirement').value}\nMessage: ${document.querySelector('textarea').value}`
    };

    const formData = {
        FirstName: document.getElementById('fname').value,
        LastName: document.getElementById('Lname').value,
        Email: document.getElementById('email').value,
        Contact: document.getElementById('contact').value,
        ReferenceVideo: document.getElementById('feference-video').value,
        ServiceRequirement: document.getElementById('Service-Requirement').value,
        Message: document.getElementById('message').value
    };

    // AJAX call to send form data
    //$.ajax({
    //    type: "POST",
    //    url: "/Home/SendEmail",
    //    data: emailData,
    //    success: function (response) {
    //        if (response.success) {
    //            // Show success modal
    //            $('#contectform').modal('show');
    //        } else {
    //            // Show warning modal with server error
    //            document.getElementById('validation-errors').innerHTML = response.message;
    //            $('#warning-message').modal('show');
    //        }
    //    },
    //    error: function () {
    //        // Show warning modal on AJAX error
    //        document.getElementById('validation-errors').innerHTML = 'An error occurred while submitting the form.';
    //        $('#warning-message').modal('show');
    //    }
    //});

    // AJAX call to save form data
    $.ajax({
        type: "POST",
        url: "/Home/SaveFormData",
        contentType: "application/json",
        data: JSON.stringify(formData),
        success: function (response) {
            if (response.success) {
                //alert(response.message);
                $('#contectform').modal('show');

                // Clear form fields after successful submission
                document.getElementById('fname').value = '';
                document.getElementById('Lname').value = '';
                document.getElementById('email').value = '';
                document.getElementById('contact').value = '';
                document.getElementById('feference-video').value = '';
                document.getElementById('Service-Requirement').value = '';
                document.getElementById('message').value = '';
            } else {
                document.getElementById('validation-errors').innerHTML = response.message;
                $('#warning-message').modal('show');
            }
        },
        error: function () {
            document.getElementById('validation-errors').innerHTML = 'An error occurred while submitting the form.';
            $('#warning-message').modal('show');
        }
    });
}

// Attach event listener to the submit button
document.getElementById('sendEmailButton').addEventListener('click', handleFormSubmission);
