
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using LoginradiusCoreSdk.Models.UserProfile;
using LoginradiusCoreSdk.Entity;
using LoginradiusCoreSdk.API;
using LoginradiusCoreSdk.Entity.APIs_Entity;
using LoginradiusCoreSdk.Models.Contact;
using LoginradiusCoreSdk.Models;
using LoginradiusCoreSdk.Models.Object;
using LoginradiusCoreSdk.Models.Album;
using LoginradiusCoreSdk.Models.CheckIn;
using LoginradiusCoreSdk.Models.Audio;
using LoginradiusCoreSdk.Models.Mention;
using LoginradiusCoreSdk.Models.Following;
using LoginradiusCoreSdk.Models.Event;
using LoginradiusCoreSdk.Models.Post;
using LoginradiusCoreSdk.Models.Company;
using LoginradiusCoreSdk.Models.Group;
using LoginradiusCoreSdk.Models.Status;
using LoginradiusCoreSdk.Models.Video;
using LoginradiusCoreSdk.Models.Photo;
using LoginradiusCoreSdk.Exception;
using LoginradiusCoreSdk.Utility.Serialization;
using LoginradiusCoreSdk.Models.Like;
using LoginradiusCoreSdk.Entity.AppSettings;
using LoginradiusCoreSdk.Models.Page;
using LoginradiusCoreSdk.Models.CloudStorageModel;

namespace Demo.Controllers
{
    public class HomeController : Controller
    {


        // GET: Home
        public ActionResult Index()
        {
            //Put your app credentials here.
			LoginRadiusAppSettings.AppSettingInitialization("", "", "");
            return View();
        }

        [HttpGet]
        public ActionResult Welcome(FormCollection form)
        {
            if (HttpContext.Session.GetString("userprofile") != null)
            {
                var userprofileJson = HttpContext.Session.GetString("userprofile");
                var userProfileData = JsonConvert.DeserializeObject<RaasUserprofile>(userprofileJson);
                return View(userProfileData);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Welcome()
        {
            //Social Login Process.
            if (!string.IsNullOrEmpty(HttpContext.Request.Form["token"]))
            {
                var lrCallback = new LoginRadiusCallback();
                var accesstoken = lrCallback.GetAccessToken(HttpContext.Request.Form["token"]);
                HttpContext.Session.SetString("access_token", Convert.ToString(accesstoken.access_token));
                ViewBag.access_token = accesstoken.access_token;
                //create client with the help of access token as parameter
                var client = new LoginRadiusClient(accesstoken);
                //create object to execute user profile api to get user profile data.
                var userprofile = new UserProfileApi();
                //To get ultimate user profile data with the help of user profile api object as parameter.
                // and pass "LoginRadiusUltimateUserProfile" model as interface to map user profile data.
                var userProfileData = client.GetResponse<LoginradiusCoreSdk.Models.UserProfile.RaasUserprofile>(userprofile);

                if (userProfileData != null)
                {
                    HttpContext.Session.SetString("userprofile", JsonConvert.SerializeObject(userProfileData));
                    HttpContext.Session.SetString("Uid", userProfileData.Uid);
                    HttpContext.Session.SetString("ID", userProfileData.ID);
                    return View(userProfileData);
                }
                return Redirect("/Home/Index");

            }

            //Traditional Login Process.
            else if (!string.IsNullOrEmpty(HttpContext.Request.Form["Access_token"]))
            {
                var userProfile = UserProfiledata(HttpContext.Request.Form["Access_token"]);
                //var  accessToken=HttpContext.Session.GetString("access_token");
                //var client = new LoginRadiusClient(accessToken);
                return View(userProfile);
            }

            //Set Password and Traditional profile registration process for social user accounts.
            else if (!string.IsNullOrEmpty(HttpContext.Request.Form["password"]) && !string.IsNullOrEmpty(HttpContext.Request.Form["confirmpassword"]) && !string.IsNullOrEmpty(HttpContext.Request.Form["emailid"]))
            {
                var _object = new LoginRadiusAccountEntity();
                var userid = HttpContext.Session.GetString("Uid");
                _object.UserCreateRegistrationProfile(userid, HttpContext.Request.Form["emailid"], HttpContext.Request.Form["password"]);

                return Content("<script language='javascript' type='text/javascript'>alert('Password has been set !'); window.location.href =window.location.href;</script>");
            }

            //Process or Function to update password of user account.
            else if (!string.IsNullOrEmpty(HttpContext.Request.Form["oldpassword"]) && !string.IsNullOrEmpty(HttpContext.Request.Form["newpassword"]) && !string.IsNullOrEmpty(HttpContext.Request.Form["confirmnewpassword"]))
            {
                try
                {
                    var _object = new LoginRadiusUserProfileEntity();
                    _object.ChangePassword(HttpContext.Request.Form["raasid"], HttpContext.Request.Form["oldpassword"], HttpContext.Request.Form["newpassword"]);
                    return Content("<script language='javascript' type='text/javascript'>alert('Password has been Changed successfully !'); window.location.href =window.location.href;</script>");
                }
                //To catch loginRadius API exception.
                catch (LoginRadiusException)
                {
                    return Content("<script language='javascript' type='text/javascript'>alert('Password can not be updated, please check your old password!' ); window.location.href = window.location.href; </script>");
                }
            }

            //Process or Function to link  a social account.
            else if (!string.IsNullOrEmpty(HttpContext.Request.Form["accounttoken"]))
            {
                var accessToken = HttpContext.Request.Form["accounttoken"];
                var client = new LoginRadiusClient(accessToken);
                var userprofile = new UserProfileApi();
                var userProfileData = client.GetResponse<RaasUserprofile>(userprofile);
                var _object = new LoginRadiusAccountEntity();
                var userid = HttpContext.Session.GetString("Uid");
                try
                {
                    var status = _object.LinkAccount(userid, userProfileData.Provider, userProfileData.ID);
                    return Content(status.isPosted ? "Social Account has been linked !" : "Social Account can not be linked !");
                }
                catch (LoginRadiusException)
                {
                    return Content("Social Account cannot be linked! it has been linked with another account");
                }
            }

            //Process or Function to unlink a social account.
            else if (!string.IsNullOrEmpty(HttpContext.Request.Form["accountunlinkname"]) && !string.IsNullOrEmpty(HttpContext.Request.Form["accountunlinkid"]))
            {
                try
                {
                    var _object = new LoginRadiusAccountEntity();
                    var userid = HttpContext.Session.GetString("Uid");
                    var status = _object.UnlinkAccount(userid, HttpContext.Request.Form["accountunlinkname"], HttpContext.Request.Form["accountunlinkid"]);


                    return Content(status.isPosted ? "Social Account has been unlinked !" : "Social Account can not be unlinked !");
                }
                catch (LoginRadiusException exception)
                {
                    return Content(" exception.Message   ");
                }

            }
            return Redirect("/Home/Index");
        }

        //Logout process to destroy the session.
        public ActionResult LogOut()
        {
            HttpContext.Session.Clear();
            TempData.Clear();
            return RedirectToAction("Index");
        }

        //Function to get Customer's Contact data of social account.
        [HttpPost]
        public ActionResult ContactData()
        {
            try
            {
                var token = HttpContext.Request.Form["access_token"];
                var client = new LoginRadiusClient(token);
                var userContacts = new ContactApi();
                var userContactsData = client.GetResponse<LoginRadiusContact>(userContacts);
                return Json(userContactsData);
            }
            catch (LoginRadiusException exception)
            {
                var error = exception.Response.Deserialize<ApiExceptionResponse>();
                return Json(error);
            }
        }

        //Function to get Customer's like data of social account.
        [HttpPost]
        public ActionResult LikeData()
        {
            try
            {
                var token = HttpContext.Request.Form["access_token"];
                var client = new LoginRadiusClient(token);
                var userLikes = new LikeApi();
                var userLikeData = client.GetResponse<List<LoginRadiusLike>>(userLikes);
                return Json(userLikeData);
            }
            catch (LoginRadiusException exception)
            {
                var error = exception.Response.Deserialize<ApiExceptionResponse>();
                return Json(error);
            }
        }

        //Function to get Customer's extended profile data of social account.
        [HttpPost]
        public ActionResult ExtendedProfile()
        {
            var userProfileJson = HttpContext.Session.GetString("userprofile");
            var userProfileData = JsonConvert.DeserializeObject<RaasUserprofile>(userProfileJson);
            return Json(userProfileData);
        }

        //Function to update the user profile.
        [HttpPost]
        public ActionResult UpdateProfile(User user)
        {
            try
            {
                var userprofileJson = HttpContext.Session.GetString("userprofile");
                var userProfileData = JsonConvert.DeserializeObject<RaasUserprofile>(userprofileJson);
                var _object = new LoginRadiusUserProfileEntity();
                var response = _object.UpdateUser(userProfileData.ID, user);
                UserProfiledata(HttpContext.Session.GetString("access_token"));
                return Content(response.isPosted ? "<script language='javascript' type='text/javascript'>alert( 'Profile has been updated !!' ); window.location.href ='/Home/Welcome'; </script>" : "<script language='javascript' type='text/javascript'>alert( 'Profile cannot be updated ,Please check parameters again !' ); window.location.href = '/Home/Welcome'; </script>");
            }
            catch (LoginRadiusException exception)
            {
                return Content("<script language='javascript' type='text/javascript'>alert( '" + exception.Response + "' ); window.location.href ='/Home/Welcome'; </script>");
            }
        }

        //Function to get Customer's album data of social account.
        [HttpPost]
        public ActionResult AlbumData()
        {
            try
            {
                var token = HttpContext.Request.Form["access_token"];
                var client = new LoginRadiusClient(token);
                var userAlbums = new AlbumApi();
                var userAlbumData = client.GetResponse<List<LoginRadiusAlbum>>(userAlbums);
                return Json(userAlbumData);
            }
            catch (LoginRadiusException exception)
            {
                var error = exception.Response.Deserialize<ApiExceptionResponse>();
                return Json(error);
            }
        }

        //Function to get Users's checkin data of social account.
        [HttpPost]
        public ActionResult CheckInData()
        {
            try
            {
                var token = HttpContext.Request.Form["access_token"];
                var client = new LoginRadiusClient(token);
                var userCheckin = new CheckInApi();
                var usercheckinData = client.GetResponse<List<LoginRadiusCheckIn>>(userCheckin);
                return Json(usercheckinData);
            }
            catch (LoginRadiusException exception)
            {
                var error = exception.Response.Deserialize<ApiExceptionResponse>();
                return Json(error);
            }

        }

        //Function to get Customer's audio data of social account.
        [HttpPost]
        public ActionResult AudioData()
        {
            try
            {
                var token = HttpContext.Request.Form["access_token"];
                var client = new LoginRadiusClient(token);
                var userAudio = new AudioApi();
                var userAudioData = client.GetResponse<List<LoginRadiusAudio>>(userAudio);
                return Json(userAudioData);
            }
            catch (LoginRadiusException exception)
            {
                var error = exception.Response.Deserialize<ApiExceptionResponse>();
                return Json(error);
            }

        }

        //Function to get Customer's Mentions data of social account.
        [HttpPost]
        public ActionResult MentionsData()
        {
            try
            {
                var token = HttpContext.Request.Form["access_token"];
                var client = new LoginRadiusClient(token);
                var userMention = new MentionApi();
                var userMentionData = client.GetResponse<List<LoginRadiusMention>>(userMention);
                return Json(userMentionData);
            }
            catch (LoginRadiusException exception)
            {
                var error = exception.Response.Deserialize<ApiExceptionResponse>();
                return Json(error);
            }
        }

        //Function to get Customer's following data of social account.
        [HttpPost]
        public ActionResult FollowingData()
        {
            try
            {
                var token = HttpContext.Request.Form["access_token"];
                var client = new LoginRadiusClient(token);
                var userFollowing = new FollowingApi();
                var userFollowingData = client.GetResponse<List<LoginRadiusFollowing>>(userFollowing);
                return Json(userFollowingData);
            }
            catch (LoginRadiusException exception)
            {
                var error = exception.Response.Deserialize<ApiExceptionResponse>();
                return Json(error);
            }
        }
        //Function to get Customer's event data of social account.
        [HttpPost]
        public ActionResult EventData()
        {
            try
            {
                var token = HttpContext.Request.Form["access_token"];
                var client = new LoginRadiusClient(token);
                var userEvent = new EventApi();
                var userEventData = client.GetResponse<List<LoginRadiusEvent>>(userEvent);
                return Json(userEventData);
            }
            catch (LoginRadiusException exception)
            {
                var error = exception.Response.Deserialize<ApiExceptionResponse>();
                return Json(error);
            }
        }
        //Function to get Customer's timeline posts data of social account.
        [HttpPost]
        public ActionResult PostsData()
        {
            try
            {
                var token = HttpContext.Request.Form["access_token"];
                var client = new LoginRadiusClient(token);
                var userPosts = new PostApi();
                var userPostsData = client.GetResponse<List<LoginRadiusPost>>(userPosts);
                return Json(userPostsData);
            }
            catch (LoginRadiusException exception)
            {
                var error = exception.Response.Deserialize<ApiExceptionResponse>();
                return Json(error);
            }
        }
        //Function to get Customer's associated companies data of social account.
        [HttpPost]
        public ActionResult CompaniesData()
        {
            try
            {
                var token = HttpContext.Request.Form["access_token"];
                var client = new LoginRadiusClient(token);
                var userCompanies = new CompanyApi();
                var userCompaniesData = client.GetResponse<List<LoginRadiusCompany>>(userCompanies);
                return Json(userCompaniesData);
            }
            catch (LoginRadiusException exception)
            {
                var error = exception.Response.Deserialize<ApiExceptionResponse>();
                return Json(error);
            }

        }

        //Function to get Customer's group data of social account.
        [HttpPost]
        public ActionResult GroupsData()
        {
            try
            {
                var token = HttpContext.Request.Form["access_token"];
                var client = new LoginRadiusClient(token);
                var userGroups = new GroupApi();
                var userGroupsData = client.GetResponse<List<LoginRadiusGroup>>(userGroups);
                return Json(userGroupsData);
            }
            catch (LoginRadiusException exception)
            {
                var error = exception.Response.Deserialize<ApiExceptionResponse>();
                return Json(error);
            }

        }

        //Function to get Customer's status data of social account.
        [HttpPost]
        public ActionResult StatusData()
        {
            try
            {
                var token = HttpContext.Request.Form["access_token"];
                var client = new LoginRadiusClient(token);
                var userStatus = new StatusApi();
                var userStatusData = client.GetResponse<List<LoginRadiusStatus>>(userStatus);
                return Json(userStatusData);
            }
            catch (LoginRadiusException exception)
            {
                var error = exception.Response.Deserialize<ApiExceptionResponse>();
                return Json(error);
            }

        }
        //Function to get Customer's videos data of social account.
        [HttpPost]
        public ActionResult VideosData()
        {
            try
            {
                var token = HttpContext.Request.Form["access_token"];
                var client = new LoginRadiusClient(token);
                var userVideos = new VideoApi(nextcursor: "0");
                var userVideosData = client.GetResponse<LoginRadiusVideo>(userVideos);
                return Json(userVideosData);
            }
            catch (LoginRadiusException exception)
            {
                var error = exception.Response.Deserialize<ApiExceptionResponse>();
                return Json(error);
            }
        }

        //Function to get Customer's profile data by Loginradius Access Token.
        public RaasUserprofile UserProfiledata(string accessToken)
        {
            HttpContext.Session.SetString("access_token", accessToken);
            ViewBag.access_token = accessToken;
            //create client with the help of access token as parameter
            var client = new LoginRadiusClient(accessToken);
            //create object to execute user profile api to get user profile data.
            var userprofile = new UserProfileApi();
            //To get ultimate user profile data with the help of user profile api object as parameter.
            // and pass "LoginRadiusUltimateUserProfile" model as interface to map user profile data.
            var userProfileData = client.GetResponse<RaasUserprofile>(userprofile);
            if (userProfileData == null) { return null; }
            HttpContext.Session.SetString("userprofile", JsonConvert.SerializeObject(userProfileData));
            HttpContext.Session.SetString("Uid", userProfileData.Uid);
            return userProfileData;
        }

        //Function to Post Customer's status on their social account.
        public string PostStatus(StatusUpdateApi values)
        {
            try
            {
                var token = HttpContext.Session.GetString("access_token");
                var client = new LoginRadiusClient(token);
                var userStatusData = client.GetResponse<LoginRadiusPostResponse>(values);
                return userStatusData.isPosted.ToString();
            }
            catch (LoginRadiusException exception)
            {
                var error = exception.Response.Deserialize<ApiExceptionResponse>();
                return error.message;
            }

        }

        //Function to get Customer's Photo data of social account.
        [HttpPost]
        public ActionResult PhotoData()
        {
            try
            {
                var albumId = HttpContext.Request.Form["AlbumID"];
                var token = HttpContext.Session.GetString("access_token");
                var client = new LoginRadiusClient(token);
                var userPhotos = new PhotoApi(albumId);
                var userPhotosData = client.GetResponse<List<LoginRadiusPhoto>>(userPhotos);
                return Json(userPhotosData);
            }
            catch (LoginRadiusException exception)
            {
                var error = exception.Response.Deserialize<ApiExceptionResponse>();
                return Json(error);
            }
        }

        //Function to get Customer's videos data of social account.
        [HttpPost]
        public string DirectMessage(MessageApi messageApi)
        {
            try
            {
                var token = HttpContext.Session.GetString("access_token");
                var client = new LoginRadiusClient(token);
                var userStatusData = client.GetResponse<LoginRadiusPostResponse>(messageApi);
                return userStatusData.isPosted.ToString();
            }
            catch (LoginRadiusException exception)
            {
                var error = exception.Response.Deserialize<ApiExceptionResponse>();
                return error.message;
            }
        }

    }
}
