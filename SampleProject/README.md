1) VS solution is in SlideShareAPI folder
2) this solution refered to SlideShareAPI project & slideshareClient projecr
3) use SlideShare sl = new SlideShare("PUT_YOUR_API_KEY", "PUT_YOUR_API_SECRET");
            //Use this to call by without making anu changed to TLS protocol 
            var str = sl.GetSlideshow(79764970);
4) SlideShare sl = new SlideShare("PUT_YOUR_API_KEY", "PUT_YOUR_API_SECRET");
            //Use following ovverided method , by passing various patameter from 0 to 5 to use diffrent diffrent protocol and it fails with SSL3 
			
			/*
			 if(TLStype==0) //to use default TLS
            {
                result = GetCommand.Execute("http://www.slideshare.net/api/2/get_slideshow", parameters);
            }
            else if (TLStype == 1) //to use default TLS
            {
                result = GetCommand.ExecuteTLSRaw("http://www.slideshare.net/api/2/get_slideshow", parameters);
            }
            else if (TLStype == 2) //to use explicitly set value as of System.Net.ServicePointManager.SecurityProtocol  as TLS
            {
                result = GetCommand.ExecuteTLS("http://www.slideshare.net/api/2/get_slideshow", parameters);
            }
            else if (TLStype == 3) //to use explicitly set value as of System.Net.ServicePointManager.SecurityProtocol  as TLS11
            {
                result = GetCommand.ExecuteTLS11("http://www.slideshare.net/api/2/get_slideshow", parameters);
            }
            else if (TLStype == 4)//to use explicitly set value as of System.Net.ServicePointManager.SecurityProtocol  as TLS12
            {
                result = GetCommand.ExecuteTLS12("http://www.slideshare.net/api/2/get_slideshow", parameters);
            }
            else if (TLStype == 5)//to use explicitly set value as of System.Net.ServicePointManager.SecurityProtocol  as SSL3
            {
                result = GetCommand.ExecuteTLSSSL3("http://www.slideshare.net/api/2/get_slideshow", parameters);
            }
			*/
			
            var str = sl.GetSlideshow(79764970, <Int Param between 0 to 5>);