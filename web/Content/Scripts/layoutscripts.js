function equalHeight(n) {
    var t = 0;
    n.each(function () {
        var n = $(this).height();
        n > t && (t = n)
    }), n.height(t)
}
function equalWidth(n) {
    var t = 0;
    n.each(function () {
        var n = $(this).width();
        n > t && (t = n)
    }), n.width(t)
}
function ReplaceAll(n, t, i) {
    for (var r = n, u = r.indexOf(t); u != -1;) r = r.replace(t, i), u = r.indexOf(t);
    return r
}
function wordcounter(n, t, i) {
    var f, r, u;
    n && (f = $(t).val().length, r = i - f, r < 0 ? (u = $(t).val(), $(t).val(u.substring(0, i))) : ($(this).attr("disabled", !1), $("#wordcounter").text("Kalan Karakter: " + r)))
}
function capWords(n) {
    var o, r, i, f, u, s;
    if (window.event ? o = window.event.keyCode : e && (o = e.which), o != 36 && o != 37) {
        for (r = $(n).val().split(" "), i = 0; i < r.length; i++) f = r[i], u = f.substr(0, 1), u == "i" && (u = "İ"), s = f.substr(1, f.length - 1), r[i] = u.toUpperCase() + s, r[i].replace(",", " ");
        $(n).val(r.join(" "))
    }
}
function SetPageSlug(n) {
    return n = ReplaceAll(n, " ", "-").toLowerCase(), n = ReplaceAll(n, "?", "-").toLowerCase(), n = ReplaceAll(n, "*", "-").toLowerCase(), n = ReplaceAll(n, "/", "-").toLowerCase(), n = ReplaceAll(n, "'", "").toLowerCase(), n = ReplaceAll(n, '"', "-").toLowerCase(), n = ReplaceAll(n, ",", "").toLowerCase(), n = ReplaceAll(n, ".", "").toLowerCase(), n = ReplaceAll(n, ":", "").toLowerCase(), n = ReplaceAll(n, ";", "").toLowerCase(), n = ReplaceAll(n, "&", "").toLowerCase(), n = ReplaceAll(n, "%", "").toLowerCase(), n = ReplaceAll(n, "+", "").toLowerCase(), n = ReplaceAll(n, "#;", "").toLowerCase(), n = ReplaceAll(n, "!", "").toLowerCase(), n = ReplaceAll(n, "ş", "s").toLowerCase(), n = ReplaceAll(n, "ç", "c").toLowerCase(), n = ReplaceAll(n, "ö", "o").toLowerCase(), n = ReplaceAll(n, "ğ", "g").toLowerCase(), n = ReplaceAll(n, "ü", "u").toLowerCase(), n = ReplaceAll(n, "Ü", "u").toLowerCase(), n = ReplaceAll(n, "Ş", "s").toLowerCase(), n = ReplaceAll(n, "Ç", "c").toLowerCase(), n = ReplaceAll(n, "Ö", "o").toLowerCase(), n = ReplaceAll(n, "Ğ", "g").toLowerCase(), n = ReplaceAll(n, "ı", "i").toLowerCase(), n = ReplaceAll(n, "I", "i").toLowerCase(), n = ReplaceAll(n, "İ", "i").toLowerCase()
}
function validateEmail(n) {
    var t = /^[a-zA-Z0-9]+[a-zA-Z0-9_.-]+[a-zA-Z0-9_-]+@[a-zA-Z0-9]+[a-zA-Z0-9.-]+[a-zA-Z0-9]+.[a-z]{2,4}$/;
    return t.test(n) ? !0 : !1
}
//function init_fields() {
//    $(".head_content input, .head_content textarea").focus(function () {
//        var n = $(this);
//        n.val() == n.attr("title") && (n.val(""), $(this).addClass("input_active"))
//    }).blur(function () {
//        var n = $(this);
//        (n.val() == "" || n.val() == null) && n.val(n.attr("title")), n.val() == n.attr("title") ? n.css("color", "#8E8E8E") : n.css("color", "#444"), $(this).removeClass("input_active")
//    })
//}
function DoLogin(n) {
    var i = $("#login_email").val(),
        r = $("#login_pass").val(),
        t = !1;
    $("#persist_box").is(":checked") && (t = !0), $.ajax({
        type: "POST",
        url: "/services/front/front.asmx/DoCompanyLogin",
        data: "{'user': '" + i + "','pass':'" + r + "','rm':'" + t + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (t) {
            var i = t.d;
            i == "Connected" ? window.location = "/Firma/anasayfa" : i == "NotConnected" ? n == "tr-TR" ? MessageBox("alert", n, "Hatalı şifre veya e-mail") : MessageBox("alert", n, "E-mail or password is wrong") : i == "error" && (n == "tr-TR" ? MessageBox("error", n, "İşlem sırasında bir hata oluştu") : MessageBox("error", n, "An error occurred"))
        },
        error: function () {
            n == "tr-TR" ? MessageBox("error", n, "İşlem Sırasında hata oluştu.") : MessageBox("error", n, "An error occurred")
        }
    })
}
function RememberPassword(n, t) {
    $.ajax({
        type: "POST",
        url: "/services/front/front.asmx/CompanyRememberPassword",
        data: "{'company': '" + t + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (t) {
            var i = t.d;
            i == "Ok" ? ($("#companyIdorName").val(""), n == "tr-TR" ? MessageBox("success", n, "Şifreniz başarıyla yenilenmiştir, Mail kutunuzu kontrol ediniz.") : MessageBox("success", n, "Password successfully changed, check the email inbox.")) : i == "Error" ? n == "tr-TR" ? MessageBox("error", n, "İşlem sırasında bir hata oluştu") : MessageBox("error", n, "An error occurred") : i == "False" && (n == "tr-TR" ? MessageBox("error", n, "Lütfen geçerli bir veri giriniz") : MessageBox("error", n, "An error occurred"))
        },
        error: function () {
            n == "tr-TR" ? MessageBox("error", n, "İşlem Sırasında hata oluştu.") : MessageBox("error", n, "An error occurred")
        }
    })
}
function ActivityInfo(n) {
    $.ajax({
        type: "POST",
        url: "/services/front/front.asmx/GetActivity",
        data: "{id: '" + n + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (n) {
            var i = n.d,
                t = "#ActivityDetail ";
            $(t + "#baslangicsaati").text(i.BaslangicZamani), $(t + "#bitissaati").text(i.BitisZamani), $(t + "#odaaciklama").text(i.OdaAciklama), $(t + "#aciklama").text(i.Aciklama), $(t + "#ilgilikisi").text(i.IlgiliKisi), $(t + "#telefon").text(i.Telefon), $(t + "#toplantitarihi").text(i.BaslangicTarihi)
        },
        error: function () {}
    })
}
function MessageBox(n, t, i) {
    var r = t == "tr-TR" ? "Tamam" : "OK";
    $.msgbox(i, {
        type: n,
        buttons: [{
            type: "submit",
            value: r
        }]
    })
}(function (n, t) {
    function ku(n) {
        var t = dt[n] = {};
        return i.each(n.split(c), function (n, i) {
            t[i] = !0
        }), t
    }
    function dr(n, r, u) {
        if (u === t && n.nodeType === 1) {
            var f = "data-" + r.replace(su, "-$1").toLowerCase();
            if (u = n.getAttribute(f), typeof u == "string") {
                try {
                    u = u === "true" ? !0 : u === "false" ? !1 : u === "null" ? null : +u + "" === u ? +u : fu.test(u) ? i.parseJSON(u) : u
                } catch (e) {}
                i.data(n, r, u)
            } else u = t
        }
        return u
    }
    function ii(n) {
        var t;
        for (t in n) if ((t !== "data" || !i.isEmptyObject(n[t])) && t !== "toJSON") return !1;
        return !0
    }
    function y() {
        return !1
    }
    function ct() {
        return !0
    }
    function d(n) {
        return !n || !n.parentNode || n.parentNode.nodeType === 11
    }
    function kr(n, t) {
        do n = n[t];
        while (n && n.nodeType !== 1);
        return n
    }
    function yr(n, t, r) {
        if (t = t || 0, i.isFunction(t)) return i.grep(n, function (n, i) {
            var u = !! t.call(n, i, n);
            return u === r
        });
        if (t.nodeType) return i.grep(n, function (n) {
            return n === t === r
        });
        if (typeof t == "string") {
            var u = i.grep(n, function (n) {
                return n.nodeType === 1
            });
            if (yo.test(t)) return i.filter(t, u, !r);
            t = i.filter(t, u)
        }
        return i.grep(n, function (n) {
            return i.inArray(n, t) >= 0 === r
        })
    }
    function br(n) {
        var i = si.split("|"),
            t = n.createDocumentFragment();
        if (t.createElement) while (i.length) t.createElement(i.pop());
        return t
    }
    function uf(n, t) {
        return n.getElementsByTagName(t)[0] || n.appendChild(n.ownerDocument.createElement(t))
    }
    function wr(n, t) {
        if (t.nodeType === 1 && i.hasData(n)) {
            var e, f, o, s = i._data(n),
                r = i._data(t, s),
                u = s.events;
            if (u) {
                delete r.handle, r.events = {};
                for (e in u) for (f = 0, o = u[e].length; f < o; f++) i.event.add(t, e, u[e][f])
            }
            r.data && (r.data = i.extend({}, r.data))
        }
    }
    function vi(n, t) {
        var r;
        t.nodeType === 1 && (t.clearAttributes && t.clearAttributes(), t.mergeAttributes && t.mergeAttributes(n), r = t.nodeName.toLowerCase(), r === "object" ? (t.parentNode && (t.outerHTML = n.outerHTML), i.support.html5Clone && n.innerHTML && !i.trim(t.innerHTML) && (t.innerHTML = n.innerHTML)) : r === "input" && nu.test(n.type) ? (t.defaultChecked = t.checked = n.checked, t.value !== n.value && (t.value = n.value)) : r === "option" ? t.selected = n.defaultSelected : r === "input" || r === "textarea" ? t.defaultValue = n.defaultValue : r === "script" && t.text !== n.text && (t.text = n.text), t.removeAttribute(i.expando))
    }
    function ht(n) {
        return typeof n.getElementsByTagName != "undefined" ? n.getElementsByTagName("*") : typeof n.querySelectorAll != "undefined" ? n.querySelectorAll("*") : []
    }
    function oi(n) {
        nu.test(n.type) && (n.defaultChecked = n.checked)
    }
    function hi(n, t) {
        if (t in n) return t;
        for (var r = t.charAt(0).toUpperCase() + t.slice(1), u = t, i = lr.length; i--;) if (t = lr[i] + r, t in n) return t;
        return u
    }
    function lt(n, t) {
        return n = t || n, i.css(n, "display") === "none" || !i.contains(n.ownerDocument, n)
    }
    function cr(n, t) {
        for (var r, o, e = [], f = 0, s = n.length; f < s; f++)(r = n[f], r.style) && (e[f] = i._data(r, "olddisplay"), t ? (!e[f] && r.style.display === "none" && (r.style.display = ""), r.style.display === "" && lt(r) && (e[f] = i._data(r, "olddisplay", tr(r.nodeName)))) : (o = u(r, "display"), !e[f] && o !== "none" && i._data(r, "olddisplay", o)));
        for (f = 0; f < s; f++)(r = n[f], r.style) && (t && r.style.display !== "none" && r.style.display !== "" || (r.style.display = t ? e[f] || "" : "none"));
        return n
    }
    function rr(n, t, i) {
        var r = to.exec(t);
        return r ? Math.max(0, r[1] - (i || 0)) + (r[2] || "px") : t
    }
    function wi(n, t, r, f) {
        for (var e = r === (f ? "border" : "content") ? 4 : t === "width" ? 1 : 0, o = 0; e < 4; e += 2) r === "margin" && (o += i.css(n, r + h[e], !0)), f ? (r === "content" && (o -= parseFloat(u(n, "padding" + h[e])) || 0), r !== "margin" && (o -= parseFloat(u(n, "border" + h[e] + "Width")) || 0)) : (o += parseFloat(u(n, "padding" + h[e])) || 0, r !== "padding" && (o += parseFloat(u(n, "border" + h[e] + "Width")) || 0));
        return o
    }
    function ur(n, t, r) {
        var f = t === "width" ? n.offsetWidth : n.offsetHeight,
            o = !0,
            e = i.support.boxSizing && i.css(n, "boxSizing") === "border-box";
        if (f <= 0 || f == null) {
            if (f = u(n, t), (f < 0 || f == null) && (f = n.style[t]), et.test(f)) return f;
            o = e && (i.support.boxSizingReliable || f === n.style[t]), f = parseFloat(f) || 0
        }
        return f + wi(n, t, r || (e ? "border" : "content"), o) + "px"
    }
    function tr(n) {
        if (ti[n]) return ti[n];
        var f = i("<" + n + ">").appendTo(r.body),
            t = f.css("display");
        return f.remove(), (t === "none" || t === "") && (v = r.body.appendChild(v || i.extend(r.createElement("iframe"), {
            frameBorder: 0,
            width: 0,
            height: 0
        })), p && v.createElement || (p = (v.contentWindow || v.contentDocument).document, p.write("<!doctype html><html><body>"), p.close()), f = p.body.appendChild(p.createElement(n)), t = u(f, "display"), r.body.removeChild(v)), ti[n] = t, t
    }
    function ri(n, t, r, u) {
        var f;
        if (i.isArray(t)) i.each(t, function (t, i) {
            r || hf.test(n) ? u(n, i) : ri(n + "[" + (typeof i == "object" ? t : "") + "]", i, r, u)
        });
        else if (r || i.type(t) !== "object") u(n, t);
        else for (f in t) ri(n + "[" + f + "]", t[f], r, u)
    }
    function di(n) {
        return function (t, r) {
            typeof t != "string" && (r = t, t = "*");
            var u, s, f, o = t.toLowerCase().split(c),
                e = 0,
                h = o.length;
            if (i.isFunction(r)) for (; e < h; e++) u = o[e], f = /^\+/.test(u), f && (u = u.substr(1) || "*"), s = n[u] = n[u] || [], s[f ? "unshift" : "push"](r)
        }
    }
    function ut(n, i, r, u, f, e) {
        f = f || i.dataTypes[0], e = e || {}, e[f] = !0;
        for (var o, c = n[f], h = 0, l = c ? c.length : 0, s = n === gt; h < l && (s || !o); h++) o = c[h](i, r, u), typeof o == "string" && (!s || e[o] ? o = t : (i.dataTypes.unshift(o), o = ut(n, i, r, u, o, e)));
        return (s || !o) && !e["*"] && (o = ut(n, i, r, u, "*", e)), o
    }
    function gi(n, r) {
        var u, f, e = i.ajaxSettings.flatOptions || {};
        for (u in r) r[u] !== t && ((e[u] ? n : f || (f = {}))[u] = r[u]);
        f && i.extend(!0, n, f)
    }
    function ro(n, i, r) {
        var o, u, e, h, s = n.contents,
            f = n.dataTypes,
            c = n.responseFields;
        for (u in c) u in r && (i[c[u]] = r[u]);
        while (f[0] === "*") f.shift(), o === t && (o = n.mimeType || i.getResponseHeader("content-type"));
        if (o) for (u in s) if (s[u] && s[u].test(o)) {
            f.unshift(u);
            break
        }
        if (f[0] in r) e = f[0];
        else {
            for (u in r) {
                if (!f[0] || n.converters[u + " " + f[0]]) {
                    e = u;
                    break
                }
                h || (h = u)
            }
            e = e || h
        }
        if (e) return e !== f[0] && f.unshift(e), r[e]
    }
    function ge(n, t) {
        var i, o, r, e, s = n.dataTypes.slice(),
            f = s[0],
            u = {}, h = 0;
        if (n.dataFilter && (t = n.dataFilter(t, n.dataType)), s[1]) for (i in n.converters) u[i.toLowerCase()] = n.converters[i];
        for (; r = s[++h];) if (r !== "*") {
            if (f !== "*" && f !== r) {
                if (i = u[f + " " + r] || u["* " + r], !i) for (o in u) if (e = o.split(" "), e[1] === r && (i = u[f + " " + e[0]] || u["* " + e[0]], i)) {
                    i === !0 ? i = u[o] : u[o] !== !0 && (r = e[0], s.splice(h--, 0, r));
                    break
                }
                if (i !== !0) if (i && n.throws) t = i(t);
                else try {
                    t = i(t)
                } catch (c) {
                    return {
                        state: "parsererror",
                        error: i ? c : "No conversion from " + f + " to " + r
                    }
                }
            }
            f = r
        }
        return {
            state: "success",
            data: t
        }
    }
    function ki() {
        try {
            return new n.XMLHttpRequest
        } catch (t) {}
    }
    function pe() {
        try {
            return new n.ActiveXObject("Microsoft.XMLHTTP")
        } catch (t) {}
    }
    function uu() {
        return setTimeout(function () {
            w = t
        }, 0), w = i.now()
    }
    function ie(n, t) {
        i.each(t, function (t, i) {
            for (var u = (k[t] || []).concat(k["*"]), r = 0, f = u.length; r < f; r++) if (u[r].call(n, t, i)) return
        })
    }
    function au(n, t, r) {
        var o, s = 0,
            l = 0,
            c = tt.length,
            f = i.Deferred().always(function () {
                delete h.elem
            }),
            h = function () {
                for (var o = w || uu(), i = Math.max(0, u.startTime + u.duration - o), s = i / u.duration || 0, t = 1 - s, r = 0, e = u.tweens.length; r < e; r++) u.tweens[r].run(t);
                return f.notifyWith(n, [u, t, i]), t < 1 && e ? i : (f.resolveWith(n, [u]), !1)
            }, u = f.promise({
                elem: n,
                props: i.extend({}, t),
                opts: i.extend(!0, {
                    specialEasing: {}
                }, r),
                originalProperties: t,
                originalOptions: r,
                startTime: w || uu(),
                duration: r.duration,
                tweens: [],
                createTween: function (t, r) {
                    var e = i.Tween(n, u.opts, t, r, u.opts.specialEasing[t] || u.opts.easing);
                    return u.tweens.push(e), e
                },
                stop: function (t) {
                    for (var i = 0, r = t ? u.tweens.length : 0; i < r; i++) u.tweens[i].run(1);
                    return t ? f.resolveWith(n, [u, t]) : f.rejectWith(n, [u, t]), this
                }
            }),
            e = u.props;
        for (ye(e, u.opts.specialEasing); s < c; s++) if (o = tt[s].call(u, n, e, u.opts), o) return o;
        return ie(u, e), i.isFunction(u.opts.start) && u.opts.start.call(n, u), i.fx.timer(i.extend(h, {
            anim: u,
            queue: u.opts.queue,
            elem: n
        })), u.progress(u.opts.progress).done(u.opts.done, u.opts.complete).fail(u.opts.fail).always(u.opts.always)
    }
    function ye(n, t) {
        var r, f, o, u, e;
        for (r in n) if (f = i.camelCase(r), o = t[f], u = n[r], i.isArray(u) && (o = u[1], u = n[r] = u[0]), r !== f && (n[f] = u, delete n[r]), e = i.cssHooks[f], e && "expand" in e) {
            u = e.expand(u), delete n[f];
            for (r in u) r in n || (n[r] = u[r], t[r] = o)
        } else t[f] = o
    }
    function ee(n, t, r) {
        var s, u, a, w, o, v, l, e, b, h = this,
            f = n.style,
            p = {}, y = [],
            c = n.nodeType && lt(n);
        r.queue || (e = i._queueHooks(n, "fx"), e.unqueued == null && (e.unqueued = 0, b = e.empty.fire, e.empty.fire = function () {
            e.unqueued || b()
        }), e.unqueued++, h.always(function () {
            h.always(function () {
                e.unqueued--, i.queue(n, "fx").length || e.empty.fire()
            })
        })), n.nodeType === 1 && ("height" in t || "width" in t) && (r.overflow = [f.overflow, f.overflowX, f.overflowY], i.css(n, "display") === "inline" && i.css(n, "float") === "none" && (!i.support.inlineBlockNeedsLayout || tr(n.nodeName) === "inline" ? f.display = "inline-block" : f.zoom = 1)), r.overflow && (f.overflow = "hidden", i.support.shrinkWrapBlocks || h.done(function () {
            f.overflow = r.overflow[0], f.overflowX = r.overflow[1], f.overflowY = r.overflow[2]
        }));
        for (s in t) if (a = t[s], wu.exec(a)) {
            if (delete t[s], v = v || a === "toggle", a === (c ? "hide" : "show")) continue;
            y.push(s)
        }
        if (w = y.length, w) for (o = i._data(n, "fxshow") || i._data(n, "fxshow", {}), ("hidden" in o) && (c = o.hidden), v && (o.hidden = !c), c ? i(n).show() : h.done(function () {
            i(n).hide()
        }), h.done(function () {
            var t;
            i.removeData(n, "fxshow", !0);
            for (t in p) i.style(n, t, p[t])
        }), s = 0; s < w; s++) u = y[s], l = h.createTween(u, c ? o[u] : 0), p[u] = o[u] || i.style(n, u), u in o || (o[u] = l.start, c && (l.end = l.start, l.start = u === "width" || u === "height" ? 1 : 0))
    }
    function f(n, t, i, r, u) {
        return new f.prototype.init(n, t, i, r, u)
    }
    function nt(n, t) {
        var u, i = {
            height: n
        }, r = 0;
        for (t = t ? 1 : 0; r < 4; r += 2 - t) u = h[r], i["margin" + u] = i["padding" + u] = n;
        return t && (i.opacity = i.width = n), i
    }
    function tu(n) {
        return i.isWindow(n) ? n : n.nodeType === 9 ? n.defaultView || n.parentWindow : !1
    }
    var iu, g, r = n.document,
        fe = n.location,
        he = n.navigator,
        ke = n.jQuery,
        se = n.$,
        gr = Array.prototype.push,
        o = Array.prototype.slice,
        ru = Array.prototype.indexOf,
        oe = Object.prototype.toString,
        yt = Object.prototype.hasOwnProperty,
        pt = String.prototype.trim,
        i = function (n, t) {
            return new i.fn.init(n, t, iu)
        }, it = /[\-+]?(?:\d*\.|)\d+(?:[eE][\-+]?\d+|)/.source,
        ve = /\S/,
        c = /\s+/,
        ae = /^[\s\uFEFF\xA0]+|[\s\uFEFF\xA0]+$/g,
        ce = /^(?:[^#<]*(<[\w\W]+>)[^>]*$|#([\w\-]*)$)/,
        ar = /^<(\w+)\s*\/?>(?:<\/\1>|)$/,
        le = /^[\],:{}\s]*$/,
        ue = /(?:^|:|,)(?:\s*\[)+/g,
        kf = /\\(?:["\\\/bfnrt]|u[\da-fA-F]{4})/g,
        df = /"[^"\\\r\n]*"|true|false|null|-?(?:\d\d*\.|)\d+(?:[eE][\-+]?\d+|)/g,
        bf = /^-ms-/,
        pf = /-([\da-z])/gi,
        wf = function (n, t) {
            return (t + "").toUpperCase()
        }, rt = function () {
            r.addEventListener ? (r.removeEventListener("DOMContentLoaded", rt, !1), i.ready()) : r.readyState === "complete" && (r.detachEvent("onreadystatechange", rt), i.ready())
        }, hu = {}, dt, fu, su, b, ot, vr, at;
    i.fn = i.prototype = {
        constructor: i,
        init: function (n, u, f) {
            var e, o, h, s;
            if (!n) return this;
            if (n.nodeType) return this.context = this[0] = n, this.length = 1, this;
            if (typeof n == "string") {
                if (e = n.charAt(0) === "<" && n.charAt(n.length - 1) === ">" && n.length >= 3 ? [null, n, null] : ce.exec(n), e && (e[1] || !u)) {
                    if (e[1]) return u = u instanceof i ? u[0] : u, s = u && u.nodeType ? u.ownerDocument || u : r, n = i.parseHTML(e[1], s, !0), ar.test(e[1]) && i.isPlainObject(u) && this.attr.call(n, u, !0), i.merge(this, n);
                    if (o = r.getElementById(e[2]), o && o.parentNode) {
                        if (o.id !== e[2]) return f.find(n);
                        this.length = 1, this[0] = o
                    }
                    return this.context = r, this.selector = n, this
                }
                return !u || u.jquery ? (u || f).find(n) : this.constructor(u).find(n)
            }
            return i.isFunction(n) ? f.ready(n) : (n.selector !== t && (this.selector = n.selector, this.context = n.context), i.makeArray(n, this))
        },
        selector: "",
        jquery: "1.8.3",
        length: 0,
        size: function () {
            return this.length
        },
        toArray: function () {
            return o.call(this)
        },
        get: function (n) {
            return n == null ? this.toArray() : n < 0 ? this[this.length + n] : this[n]
        },
        pushStack: function (n, t, r) {
            var u = i.merge(this.constructor(), n);
            return u.prevObject = this, u.context = this.context, t === "find" ? u.selector = this.selector + (this.selector ? " " : "") + r : t && (u.selector = this.selector + "." + t + "(" + r + ")"), u
        },
        each: function (n, t) {
            return i.each(this, n, t)
        },
        ready: function (n) {
            return i.ready.promise().done(n), this
        },
        eq: function (n) {
            return n = +n, n === -1 ? this.slice(n) : this.slice(n, n + 1)
        },
        first: function () {
            return this.eq(0)
        },
        last: function () {
            return this.eq(-1)
        },
        slice: function () {
            return this.pushStack(o.apply(this, arguments), "slice", o.call(arguments).join(","))
        },
        map: function (n) {
            return this.pushStack(i.map(this, function (t, i) {
                return n.call(t, i, t)
            }))
        },
        end: function () {
            return this.prevObject || this.constructor(null)
        },
        push: gr,
        sort: [].sort,
        splice: [].splice
    }, i.fn.init.prototype = i.fn, i.extend = i.fn.extend = function () {
        var s, e, u, r, h, c, n = arguments[0] || {}, f = 1,
            l = arguments.length,
            o = !1;
        for (typeof n == "boolean" && (o = n, n = arguments[1] || {}, f = 2), typeof n != "object" && !i.isFunction(n) && (n = {}), l === f && (n = this, --f); f < l; f++) if ((s = arguments[f]) != null) for (e in s)(u = n[e], r = s[e], n !== r) && (o && r && (i.isPlainObject(r) || (h = i.isArray(r))) ? (h ? (h = !1, c = u && i.isArray(u) ? u : []) : c = u && i.isPlainObject(u) ? u : {}, n[e] = i.extend(o, c, r)) : r !== t && (n[e] = r));
        return n
    }, i.extend({
        noConflict: function (t) {
            return n.$ === i && (n.$ = se), t && n.jQuery === i && (n.jQuery = ke), i
        },
        isReady: !1,
        readyWait: 1,
        holdReady: function (n) {
            n ? i.readyWait++ : i.ready(!0)
        },
        ready: function (n) {
            if (n === !0 ? !--i.readyWait : !i.isReady) {
                if (!r.body) return setTimeout(i.ready, 1);
                (i.isReady = !0, n !== !0 && --i.readyWait > 0) || (g.resolveWith(r, [i]), i.fn.trigger && i(r).trigger("ready").off("ready"))
            }
        },
        isFunction: function (n) {
            return i.type(n) === "function"
        },
        isArray: Array.isArray || function (n) {
            return i.type(n) === "array"
        },
        isWindow: function (n) {
            return n != null && n == n.window
        },
        isNumeric: function (n) {
            return !isNaN(parseFloat(n)) && isFinite(n)
        },
        type: function (n) {
            return n == null ? String(n) : hu[oe.call(n)] || "object"
        },
        isPlainObject: function (n) {
            if (!n || i.type(n) !== "object" || n.nodeType || i.isWindow(n)) return !1;
            try {
                if (n.constructor && !yt.call(n, "constructor") && !yt.call(n.constructor.prototype, "isPrototypeOf")) return !1
            } catch (u) {
                return !1
            }
            var r;
            for (r in n);
            return r === t || yt.call(n, r)
        },
        isEmptyObject: function (n) {
            var t;
            for (t in n) return !1;
            return !0
        },
        error: function (n) {
            throw new Error(n);
        },
        parseHTML: function (n, t, u) {
            var f;
            return !n || typeof n != "string" ? null : (typeof t == "boolean" && (u = t, t = 0), t = t || r, (f = ar.exec(n)) ? [t.createElement(f[1])] : (f = i.buildFragment([n], t, u ? null : []), i.merge([], (f.cacheable ? i.clone(f.fragment) : f.fragment).childNodes)))
        },
        parseJSON: function (t) {
            if (!t || typeof t != "string") return null;
            if (t = i.trim(t), n.JSON && n.JSON.parse) return n.JSON.parse(t);
            if (le.test(t.replace(kf, "@").replace(df, "]").replace(ue, ""))) return new Function("return " + t)();
            i.error("Invalid JSON: " + t)
        },
        parseXML: function (r) {
            var u, f;
            if (!r || typeof r != "string") return null;
            try {
                n.DOMParser ? (f = new DOMParser, u = f.parseFromString(r, "text/xml")) : (u = new ActiveXObject("Microsoft.XMLDOM"), u.async = "false", u.loadXML(r))
            } catch (e) {
                u = t
            }
            return (!u || !u.documentElement || u.getElementsByTagName("parsererror").length) && i.error("Invalid XML: " + r), u
        },
        noop: function () {},
        globalEval: function (t) {
            t && ve.test(t) && (n.execScript || function (t) {
                n.eval.call(n, t)
            })(t)
        },
        camelCase: function (n) {
            return n.replace(bf, "ms-").replace(pf, wf)
        },
        nodeName: function (n, t) {
            return n.nodeName && n.nodeName.toLowerCase() === t.toLowerCase()
        },
        each: function (n, r, u) {
            var e, f = 0,
                o = n.length,
                s = o === t || i.isFunction(n);
            if (u) {
                if (s) {
                    for (e in n) if (r.apply(n[e], u) === !1) break
                } else for (; f < o;) if (r.apply(n[f++], u) === !1) break
            } else if (s) {
                for (e in n) if (r.call(n[e], e, n[e]) === !1) break
            } else for (; f < o;) if (r.call(n[f], f, n[f++]) === !1) break;
            return n
        },
        trim: pt && !pt.call("﻿ ") ? function (n) {
            return n == null ? "" : pt.call(n)
        } : function (n) {
            return n == null ? "" : (n + "").replace(ae, "")
        },
        makeArray: function (n, t) {
            var r, u = t || [];
            return n != null && (r = i.type(n), n.length == null || r === "string" || r === "function" || r === "regexp" || i.isWindow(n) ? gr.call(u, n) : i.merge(u, n)), u
        },
        inArray: function (n, t, i) {
            var r;
            if (t) {
                if (ru) return ru.call(t, n, i);
                for (r = t.length, i = i ? i < 0 ? Math.max(0, r + i) : i : 0; i < r; i++) if (i in t && t[i] === n) return i
            }
            return -1
        },
        merge: function (n, i) {
            var f = i.length,
                u = n.length,
                r = 0;
            if (typeof f == "number") for (; r < f; r++) n[u++] = i[r];
            else while (i[r] !== t) n[u++] = i[r++];
            return n.length = u, n
        },
        grep: function (n, t, i) {
            var f, u = [],
                r = 0,
                e = n.length;
            for (i = !! i; r < e; r++) f = !! t(n[r], r), i !== f && u.push(n[r]);
            return u
        },
        map: function (n, r, u) {
            var o, h, f = [],
                s = 0,
                e = n.length,
                c = n instanceof i || e !== t && typeof e == "number" && (e > 0 && n[0] && n[e - 1] || e === 0 || i.isArray(n));
            if (c) for (; s < e; s++) o = r(n[s], s, u), o != null && (f[f.length] = o);
            else for (h in n) o = r(n[h], h, u), o != null && (f[f.length] = o);
            return f.concat.apply([], f)
        },
        guid: 1,
        proxy: function (n, r) {
            var f, e, u;
            return typeof r == "string" && (f = n[r], r = n, n = f), i.isFunction(n) ? (e = o.call(arguments, 2), u = function () {
                return n.apply(r, e.concat(o.call(arguments)))
            }, u.guid = n.guid = n.guid || i.guid++, u) : t
        },
        access: function (n, r, u, f, e, o, s) {
            var c, a = u == null,
                h = 0,
                l = n.length;
            if (u && typeof u == "object") {
                for (h in u) i.access(n, r, h, u[h], 1, o, f);
                e = 1
            } else if (f !== t) {
                if (c = s === t && i.isFunction(f), a && (c ? (c = r, r = function (n, t, r) {
                    return c.call(i(n), r)
                }) : (r.call(n, f), r = null)), r) for (; h < l; h++) r(n[h], u, c ? f.call(n[h], h, r(n[h], u)) : f, s);
                e = 1
            }
            return e ? n : a ? r.call(n) : l ? r(n[0], u) : o
        },
        now: function () {
            return +new Date
        }
    }), i.ready.promise = function (t) {
        if (!g) if (g = i.Deferred(), r.readyState === "complete") setTimeout(i.ready, 1);
        else if (r.addEventListener) r.addEventListener("DOMContentLoaded", rt, !1), n.addEventListener("load", i.ready, !1);
        else {
            r.attachEvent("onreadystatechange", rt), n.attachEvent("onload", i.ready);
            var u = !1;
            try {
                u = n.frameElement == null && r.documentElement
            } catch (e) {}
            u && u.doScroll && function f() {
                if (!i.isReady) {
                    try {
                        u.doScroll("left")
                    } catch (n) {
                        return setTimeout(f, 50)
                    }
                    i.ready()
                }
            }()
        }
        return g.promise(t)
    }, i.each("Boolean Number String Function Array Date RegExp Object".split(" "), function (n, t) {
        hu["[object " + t + "]"] = t.toLowerCase()
    }), iu = i(r), dt = {}, i.Callbacks = function (n) {
        n = typeof n == "string" ? dt[n] || ku(n) : i.extend({}, n);
        var f, c, s, a, h, e, r = [],
            u = !n.once && [],
            l = function (t) {
                for (f = n.memory && t, c = !0, e = a || 0, a = 0, h = r.length, s = !0; r && e < h; e++) if (r[e].apply(t[0], t[1]) === !1 && n.stopOnFalse) {
                    f = !1;
                    break
                }
                s = !1, r && (u ? u.length && l(u.shift()) : f ? r = [] : o.disable())
            }, o = {
                add: function () {
                    if (r) {
                        var u = r.length;
                        (function t(u) {
                            i.each(u, function (u, f) {
                                var e = i.type(f);
                                e === "function" ? (!n.unique || !o.has(f)) && r.push(f) : f && f.length && e !== "string" && t(f)
                            })
                        })(arguments), s ? h = r.length : f && (a = u, l(f))
                    }
                    return this
                },
                remove: function () {
                    return r && i.each(arguments, function (n, t) {
                        for (var u;
                        (u = i.inArray(t, r, u)) > -1;) r.splice(u, 1), s && (u <= h && h--, u <= e && e--)
                    }), this
                },
                has: function (n) {
                    return i.inArray(n, r) > -1
                },
                empty: function () {
                    return r = [], this
                },
                disable: function () {
                    return r = u = f = t, this
                },
                disabled: function () {
                    return !r
                },
                lock: function () {
                    return u = t, f || o.disable(), this
                },
                locked: function () {
                    return !u
                },
                fireWith: function (n, t) {
                    return t = t || [], t = [n, t.slice ? t.slice() : t], r && (!c || u) && (s ? u.push(t) : l(t)), this
                },
                fire: function () {
                    return o.fireWith(this, arguments), this
                },
                fired: function () {
                    return !!c
                }
            };
        return o
    }, i.extend({
        Deferred: function (n) {
            var u = [
                ["resolve", "done", i.Callbacks("once memory"), "resolved"],
                ["reject", "fail", i.Callbacks("once memory"), "rejected"],
                ["notify", "progress", i.Callbacks("memory")]
            ],
                f = "pending",
                r = {
                    state: function () {
                        return f
                    },
                    always: function () {
                        return t.done(arguments).fail(arguments), this
                    },
                    then: function () {
                        var n = arguments;
                        return i.Deferred(function (r) {
                            i.each(u, function (u, f) {
                                var o = f[0],
                                    e = n[u];
                                t[f[1]](i.isFunction(e) ? function () {
                                    var n = e.apply(this, arguments);
                                    n && i.isFunction(n.promise) ? n.promise().done(r.resolve).fail(r.reject).progress(r.notify) : r[o + "With"](this === t ? r : this, [n])
                                } : r[o])
                            }), n = null
                        }).promise()
                    },
                    promise: function (n) {
                        return n != null ? i.extend(n, r) : r
                    }
                }, t = {};
            return r.pipe = r.then, i.each(u, function (n, i) {
                var e = i[2],
                    o = i[3];
                r[i[1]] = e.add, o && e.add(function () {
                    f = o
                }, u[n ^ 1][2].disable, u[2][2].lock), t[i[0]] = e.fire, t[i[0] + "With"] = e.fireWith
            }), r.promise(t), n && n.call(t, t), t
        },
        when: function (n) {
            var r = 0,
                u = o.call(arguments),
                t = u.length,
                e = t !== 1 || n && i.isFunction(n.promise) ? t : 0,
                f = e === 1 ? n : i.Deferred(),
                c = function (n, t, i) {
                    return function (r) {
                        t[n] = this, i[n] = arguments.length > 1 ? o.call(arguments) : r, i === h ? f.notifyWith(t, i) : --e || f.resolveWith(t, i)
                    }
                }, h, l, s;
            if (t > 1) for (h = new Array(t), l = new Array(t), s = new Array(t); r < t; r++) u[r] && i.isFunction(u[r].promise) ? u[r].promise().done(c(r, s, u)).fail(f.reject).progress(c(r, l, h)) : --e;
            return e || f.resolveWith(s, u), f.promise()
        }
    }), i.support = function () {
        var u, h, e, l, c, f, o, a, v, s, y, t = r.createElement("div");
        if (t.setAttribute("className", "t"), t.innerHTML = "  <link/><table></table><a href='/a'>a</a><input type='checkbox'/>", h = t.getElementsByTagName("*"), e = t.getElementsByTagName("a")[0], !h || !e || !h.length) return {};
        l = r.createElement("select"), c = l.appendChild(r.createElement("option")), f = t.getElementsByTagName("input")[0], e.style.cssText = "top:1px;float:left;opacity:.5", u = {
            leadingWhitespace: t.firstChild.nodeType === 3,
            tbody: !t.getElementsByTagName("tbody").length,
            htmlSerialize: !! t.getElementsByTagName("link").length,
            style: /top/.test(e.getAttribute("style")),
            hrefNormalized: e.getAttribute("href") === "/a",
            opacity: /^0.5/.test(e.style.opacity),
            cssFloat: !! e.style.cssFloat,
            checkOn: f.value === "on",
            optSelected: c.selected,
            getSetAttribute: t.className !== "t",
            enctype: !! r.createElement("form").enctype,
            html5Clone: r.createElement("nav").cloneNode(!0).outerHTML !== "<:nav></:nav>",
            boxModel: r.compatMode === "CSS1Compat",
            submitBubbles: !0,
            changeBubbles: !0,
            focusinBubbles: !1,
            deleteExpando: !0,
            noCloneEvent: !0,
            inlineBlockNeedsLayout: !1,
            shrinkWrapBlocks: !1,
            reliableMarginRight: !0,
            boxSizingReliable: !0,
            pixelPosition: !1
        }, f.checked = !0, u.noCloneChecked = f.cloneNode(!0).checked, l.disabled = !0, u.optDisabled = !c.disabled;
        try {
            delete t.test
        } catch (p) {
            u.deleteExpando = !1
        }
        if (!t.addEventListener && t.attachEvent && t.fireEvent && (t.attachEvent("onclick", y = function () {
            u.noCloneEvent = !1
        }), t.cloneNode(!0).fireEvent("onclick"), t.detachEvent("onclick", y)), f = r.createElement("input"), f.value = "t", f.setAttribute("type", "radio"), u.radioValue = f.value === "t", f.setAttribute("checked", "checked"), f.setAttribute("name", "t"), t.appendChild(f), o = r.createDocumentFragment(), o.appendChild(t.lastChild), u.checkClone = o.cloneNode(!0).cloneNode(!0).lastChild.checked, u.appendChecked = f.checked, o.removeChild(f), o.appendChild(t), t.attachEvent) for (v in {
                submit: !0,
                change: !0,
                focusin: !0
        }) a = "on" + v, s = a in t, s || (t.setAttribute(a, "return;"), s = typeof t[a] == "function"), u[v + "Bubbles"] = s;
        return i(function () {
            var e, t, f, i, h = "padding:0;margin:0;border:0;display:block;overflow:hidden;",
                o = r.getElementsByTagName("body")[0];
            o && (e = r.createElement("div"), e.style.cssText = "visibility:hidden;border:0;width:0;height:0;position:static;top:0;margin-top:1px", o.insertBefore(e, o.firstChild), t = r.createElement("div"), e.appendChild(t), t.innerHTML = "<table><tr><td></td><td>t</td></tr></table>", f = t.getElementsByTagName("td"), f[0].style.cssText = "padding:0;margin:0;border:0;display:none", s = f[0].offsetHeight === 0, f[0].style.display = "", f[1].style.display = "none", u.reliableHiddenOffsets = s && f[0].offsetHeight === 0, t.innerHTML = "", t.style.cssText = "box-sizing:border-box;-moz-box-sizing:border-box;-webkit-box-sizing:border-box;padding:1px;border:1px;display:block;width:4px;margin-top:1%;position:absolute;top:1%;", u.boxSizing = t.offsetWidth === 4, u.doesNotIncludeMarginInBodyOffset = o.offsetTop !== 1, n.getComputedStyle && (u.pixelPosition = (n.getComputedStyle(t, null) || {}).top !== "1%", u.boxSizingReliable = (n.getComputedStyle(t, null) || {
                width: "4px"
            }).width === "4px", i = r.createElement("div"), i.style.cssText = t.style.cssText = h, i.style.marginRight = i.style.width = "0", t.style.width = "1px", t.appendChild(i), u.reliableMarginRight = !parseFloat((n.getComputedStyle(i, null) || {}).marginRight)), typeof t.style.zoom != "undefined" && (t.innerHTML = "", t.style.cssText = h + "width:1px;padding:1px;display:inline;zoom:1", u.inlineBlockNeedsLayout = t.offsetWidth === 3, t.style.display = "block", t.style.overflow = "visible", t.innerHTML = "<div></div>", t.firstChild.style.width = "5px", u.shrinkWrapBlocks = t.offsetWidth !== 3, e.style.zoom = 1), o.removeChild(e), e = t = f = i = null)
        }), o.removeChild(t), h = e = l = c = f = o = t = null, u
    }(), fu = /(?:\{[\s\S]*\}|\[[\s\S]*\])$/, su = /([A-Z])/g, i.extend({
        cache: {},
        deletedIds: [],
        uuid: 0,
        expando: "jQuery" + (i.fn.jquery + Math.random()).replace(/\D/g, ""),
        noData: {
            embed: !0,
            object: "clsid:D27CDB6E-AE6D-11cf-96B8-444553540000",
            applet: !0
        },
        hasData: function (n) {
            return n = n.nodeType ? i.cache[n[i.expando]] : n[i.expando], !! n && !ii(n)
        },
        data: function (n, r, u, f) {
            if (i.acceptData(n)) {
                var s, h, c = i.expando,
                    a = typeof r == "string",
                    l = n.nodeType,
                    o = l ? i.cache : n,
                    e = l ? n[c] : n[c] && c;
                if (e && o[e] && (f || o[e].data) || !a || u !== t) return e || (l ? n[c] = e = i.deletedIds.pop() || i.guid++ : e = c), o[e] || (o[e] = {}, l || (o[e].toJSON = i.noop)), (typeof r == "object" || typeof r == "function") && (f ? o[e] = i.extend(o[e], r) : o[e].data = i.extend(o[e].data, r)), s = o[e], f || (s.data || (s.data = {}), s = s.data), u !== t && (s[i.camelCase(r)] = u), a ? (h = s[r], h == null && (h = s[i.camelCase(r)])) : h = s, h
            }
        },
        removeData: function (n, t, r) {
            if (i.acceptData(n)) {
                var e, o, h, s = n.nodeType,
                    u = s ? i.cache : n,
                    f = s ? n[i.expando] : i.expando;
                if (u[f]) {
                    if (t && (e = r ? u[f] : u[f].data, e)) {
                        for (i.isArray(t) || (t in e ? t = [t] : (t = i.camelCase(t), t = t in e ? [t] : t.split(" "))), o = 0, h = t.length; o < h; o++) delete e[t[o]];
                        if (!(r ? ii : i.isEmptyObject)(e)) return
                    }(r || (delete u[f].data, ii(u[f]))) && (s ? i.cleanData([n], !0) : i.support.deleteExpando || u != u.window ? delete u[f] : u[f] = null)
                }
            }
        },
        _data: function (n, t, r) {
            return i.data(n, t, r, !0)
        },
        acceptData: function (n) {
            var t = n.nodeName && i.noData[n.nodeName.toLowerCase()];
            return !t || t !== !0 && n.getAttribute("classid") === t
        }
    }), i.fn.extend({
        data: function (n, r) {
            var u, s, c, o, l, e = this[0],
                h = 0,
                f = null;
            if (n === t) {
                if (this.length && (f = i.data(e), e.nodeType === 1 && !i._data(e, "parsedAttrs"))) {
                    for (c = e.attributes, l = c.length; h < l; h++) o = c[h].name, o.indexOf("data-") || (o = i.camelCase(o.substring(5)), dr(e, o, f[o]));
                    i._data(e, "parsedAttrs", !0)
                }
                return f
            }
            return typeof n == "object" ? this.each(function () {
                i.data(this, n)
            }) : (u = n.split(".", 2), u[1] = u[1] ? "." + u[1] : "", s = u[1] + "!", i.access(this, function (r) {
                if (r === t) return f = this.triggerHandler("getData" + s, [u[0]]), f === t && e && (f = i.data(e, n), f = dr(e, n, f)), f === t && u[1] ? this.data(u[0]) : f;
                u[1] = r, this.each(function () {
                    var t = i(this);
                    t.triggerHandler("setData" + s, u), i.data(this, n, r), t.triggerHandler("changeData" + s, u)
                })
            }, null, r, arguments.length > 1, null, !1))
        },
        removeData: function (n) {
            return this.each(function () {
                i.removeData(this, n)
            })
        }
    }), i.extend({
        queue: function (n, t, r) {
            var u;
            if (n) return t = (t || "fx") + "queue", u = i._data(n, t), r && (!u || i.isArray(r) ? u = i._data(n, t, i.makeArray(r)) : u.push(r)), u || []
        },
        dequeue: function (n, t) {
            t = t || "fx";
            var f = i.queue(n, t),
                e = f.length,
                r = f.shift(),
                u = i._queueHooks(n, t),
                o = function () {
                    i.dequeue(n, t)
                };
            r === "inprogress" && (r = f.shift(), e--), r && (t === "fx" && f.unshift("inprogress"), delete u.stop, r.call(n, o, u)), !e && u && u.empty.fire()
        },
        _queueHooks: function (n, t) {
            var r = t + "queueHooks";
            return i._data(n, r) || i._data(n, r, {
                empty: i.Callbacks("once memory").add(function () {
                    i.removeData(n, t + "queue", !0), i.removeData(n, r, !0)
                })
            })
        }
    }), i.fn.extend({
        queue: function (n, r) {
            var u = 2;
            return typeof n != "string" && (r = n, n = "fx", u--), arguments.length < u ? i.queue(this[0], n) : r === t ? this : this.each(function () {
                var t = i.queue(this, n, r);
                i._queueHooks(this, n), n === "fx" && t[0] !== "inprogress" && i.dequeue(this, n)
            })
        },
        dequeue: function (n) {
            return this.each(function () {
                i.dequeue(this, n)
            })
        },
        delay: function (n, t) {
            return n = i.fx ? i.fx.speeds[n] || n : n, t = t || "fx", this.queue(t, function (t, i) {
                var r = setTimeout(t, n);
                i.stop = function () {
                    clearTimeout(r)
                }
            })
        },
        clearQueue: function (n) {
            return this.queue(n || "fx", [])
        },
        promise: function (n, r) {
            var u, s = 1,
                h = i.Deferred(),
                f = this,
                o = this.length,
                e = function () {
                    --s || h.resolveWith(f, [f])
                };
            for (typeof n != "string" && (r = n, n = t), n = n || "fx"; o--;) u = i._data(f[o], n + "queueHooks"), u && u.empty && (s++, u.empty.add(e));
            return e(), h.promise(r)
        }
    });
    var s, cu, eu, lu = /[\t\r\n]/g,
        re = /\r/g,
        te = /^(?:button|input)$/i,
        gf = /^(?:button|input|object|select|textarea)$/i,
        ne = /^a(?:rea|)$/i,
        yi = /^(?:autofocus|autoplay|async|checked|controls|defer|disabled|hidden|loop|multiple|open|readonly|required|scoped|selected)$/i,
        bi = i.support.getSetAttribute;
    i.fn.extend({
        attr: function (n, t) {
            return i.access(this, i.attr, n, t, arguments.length > 1)
        },
        removeAttr: function (n) {
            return this.each(function () {
                i.removeAttr(this, n)
            })
        },
        prop: function (n, t) {
            return i.access(this, i.prop, n, t, arguments.length > 1)
        },
        removeProp: function (n) {
            return n = i.propFix[n] || n, this.each(function () {
                try {
                    this[n] = t, delete this[n]
                } catch (i) {}
            })
        },
        addClass: function (n) {
            var u, e, s, t, f, r, o;
            if (i.isFunction(n)) return this.each(function (t) {
                i(this).addClass(n.call(this, t, this.className))
            });
            if (n && typeof n == "string") for (u = n.split(c), e = 0, s = this.length; e < s; e++) if (t = this[e], t.nodeType === 1) if (t.className || u.length !== 1) {
                for (f = " " + t.className + " ", r = 0, o = u.length; r < o; r++) f.indexOf(" " + u[r] + " ") < 0 && (f += u[r] + " ");
                t.className = i.trim(f)
            } else t.className = n;
            return this
        },
        removeClass: function (n) {
            var o, f, r, u, h, e, s;
            if (i.isFunction(n)) return this.each(function (t) {
                i(this).removeClass(n.call(this, t, this.className))
            });
            if (n && typeof n == "string" || n === t) for (o = (n || "").split(c), e = 0, s = this.length; e < s; e++) if (r = this[e], r.nodeType === 1 && r.className) {
                for (f = (" " + r.className + " ").replace(lu, " "), u = 0, h = o.length; u < h; u++) while (f.indexOf(" " + o[u] + " ") >= 0) f = f.replace(" " + o[u] + " ", " ");
                r.className = n ? i.trim(f) : ""
            }
            return this
        },
        toggleClass: function (n, t) {
            var r = typeof n,
                u = typeof t == "boolean";
            return i.isFunction(n) ? this.each(function (r) {
                i(this).toggleClass(n.call(this, r, this.className, t), t)
            }) : this.each(function () {
                if (r === "string") for (var e, h = 0, o = i(this), f = t, s = n.split(c); e = s[h++];) f = u ? f : !o.hasClass(e), o[f ? "addClass" : "removeClass"](e);
                else(r === "undefined" || r === "boolean") && (this.className && i._data(this, "__className__", this.className), this.className = this.className || n === !1 ? "" : i._data(this, "__className__") || "")
            })
        },
        hasClass: function (n) {
            for (var r = " " + n + " ", t = 0, i = this.length; t < i; t++) if (this[t].nodeType === 1 && (" " + this[t].className + " ").replace(lu, " ").indexOf(r) >= 0) return !0;
            return !1
        },
        val: function (n) {
            var r, u, e, f = this[0];
            return arguments.length ? (e = i.isFunction(n), this.each(function (u) {
                var f, o = i(this);
                this.nodeType === 1 && (f = e ? n.call(this, u, o.val()) : n, f == null ? f = "" : typeof f == "number" ? f += "" : i.isArray(f) && (f = i.map(f, function (n) {
                    return n == null ? "" : n + ""
                })), r = i.valHooks[this.type] || i.valHooks[this.nodeName.toLowerCase()], r && "set" in r && r.set(this, f, "value") !== t || (this.value = f))
            })) : f ? (r = i.valHooks[f.type] || i.valHooks[f.nodeName.toLowerCase()], r && "get" in r && (u = r.get(f, "value")) !== t ? u : (u = f.value, typeof u == "string" ? u.replace(re, "") : u == null ? "" : u)) : void 0
        }
    }), i.extend({
        valHooks: {
            option: {
                get: function (n) {
                    var t = n.attributes.value;
                    return !t || t.specified ? n.value : n.text
                }
            },
            select: {
                get: function (n) {
                    for (var e, t, o = n.options, r = n.selectedIndex, f = n.type === "select-one" || r < 0, h = f ? null : [], s = f ? r + 1 : o.length, u = r < 0 ? s : f ? r : 0; u < s; u++) if (t = o[u], (t.selected || u === r) && (i.support.optDisabled ? !t.disabled : t.getAttribute("disabled") === null) && (!t.parentNode.disabled || !i.nodeName(t.parentNode, "optgroup"))) {
                        if (e = i(t).val(), f) return e;
                        h.push(e)
                    }
                    return h
                },
                set: function (n, t) {
                    var r = i.makeArray(t);
                    return i(n).find("option").each(function () {
                        this.selected = i.inArray(i(this).val(), r) >= 0
                    }), r.length || (n.selectedIndex = -1), r
                }
            }
        },
        attrFn: {},
        attr: function (n, r, u, f) {
            var o, e, c, h = n.nodeType;
            if (n && h !== 3 && h !== 8 && h !== 2) {
                if (f && i.isFunction(i.fn[r])) return i(n)[r](u);
                if (typeof n.getAttribute == "undefined") return i.prop(n, r, u);
                if (c = h !== 1 || !i.isXMLDoc(n), c && (r = r.toLowerCase(), e = i.attrHooks[r] || (yi.test(r) ? cu : s)), u !== t) {
                    if (u === null) {
                        i.removeAttr(n, r);
                        return
                    }
                    return e && "set" in e && c && (o = e.set(n, u, r)) !== t ? o : (n.setAttribute(r, u + ""), u)
                }
                return e && "get" in e && c && (o = e.get(n, r)) !== null ? o : (o = n.getAttribute(r), o === null ? t : o)
            }
        },
        removeAttr: function (n, t) {
            var u, o, r, e, f = 0;
            if (t && n.nodeType === 1) for (o = t.split(c); f < o.length; f++) r = o[f], r && (u = i.propFix[r] || r, e = yi.test(r), e || i.attr(n, r, ""), n.removeAttribute(bi ? r : u), e && u in n && (n[u] = !1))
        },
        attrHooks: {
            type: {
                set: function (n, t) {
                    if (te.test(n.nodeName) && n.parentNode) i.error("type property can't be changed");
                    else if (!i.support.radioValue && t === "radio" && i.nodeName(n, "input")) {
                        var r = n.value;
                        return n.setAttribute("type", t), r && (n.value = r), t
                    }
                }
            },
            value: {
                get: function (n, t) {
                    return s && i.nodeName(n, "button") ? s.get(n, t) : t in n ? n.value : null
                },
                set: function (n, t, r) {
                    if (s && i.nodeName(n, "button")) return s.set(n, t, r);
                    n.value = t
                }
            }
        },
        propFix: {
            tabindex: "tabIndex",
            readonly: "readOnly",
            "for": "htmlFor",
            "class": "className",
            maxlength: "maxLength",
            cellspacing: "cellSpacing",
            cellpadding: "cellPadding",
            rowspan: "rowSpan",
            colspan: "colSpan",
            usemap: "useMap",
            frameborder: "frameBorder",
            contenteditable: "contentEditable"
        },
        prop: function (n, r, u) {
            var o, f, s, e = n.nodeType;
            if (n && e !== 3 && e !== 8 && e !== 2) return s = e !== 1 || !i.isXMLDoc(n), s && (r = i.propFix[r] || r, f = i.propHooks[r]), u !== t ? f && "set" in f && (o = f.set(n, u, r)) !== t ? o : n[r] = u : f && "get" in f && (o = f.get(n, r)) !== null ? o : n[r]
        },
        propHooks: {
            tabIndex: {
                get: function (n) {
                    var i = n.getAttributeNode("tabindex");
                    return i && i.specified ? parseInt(i.value, 10) : gf.test(n.nodeName) || ne.test(n.nodeName) && n.href ? 0 : t
                }
            }
        }
    }), cu = {
        get: function (n, r) {
            var f, u = i.prop(n, r);
            return u === !0 || typeof u != "boolean" && (f = n.getAttributeNode(r)) && f.nodeValue !== !1 ? r.toLowerCase() : t
        },
        set: function (n, t, r) {
            var u;
            return t === !1 ? i.removeAttr(n, r) : (u = i.propFix[r] || r, u in n && (n[u] = !0), n.setAttribute(r, r.toLowerCase())), r
        }
    }, bi || (eu = {
        name: !0,
        id: !0,
        coords: !0
    }, s = i.valHooks.button = {
        get: function (n, i) {
            var r;
            return r = n.getAttributeNode(i), r && (eu[i] ? r.value !== "" : r.specified) ? r.value : t
        },
        set: function (n, t, i) {
            var u = n.getAttributeNode(i);
            return u || (u = r.createAttribute(i), n.setAttributeNode(u)), u.value = t + ""
        }
    }, i.each(["width", "height"], function (n, t) {
        i.attrHooks[t] = i.extend(i.attrHooks[t], {
            set: function (n, i) {
                if (i === "") return n.setAttribute(t, "auto"), i
            }
        })
    }), i.attrHooks.contenteditable = {
        get: s.get,
        set: function (n, t, i) {
            t === "" && (t = "false"), s.set(n, t, i)
        }
    }), i.support.hrefNormalized || i.each(["href", "src", "width", "height"], function (n, r) {
        i.attrHooks[r] = i.extend(i.attrHooks[r], {
            get: function (n) {
                var i = n.getAttribute(r, 2);
                return i === null ? t : i
            }
        })
    }), i.support.style || (i.attrHooks.style = {
        get: function (n) {
            return n.style.cssText.toLowerCase() || t
        },
        set: function (n, t) {
            return n.style.cssText = t + ""
        }
    }), i.support.optSelected || (i.propHooks.selected = i.extend(i.propHooks.selected, {
        get: function (n) {
            var t = n.parentNode;
            return t && (t.selectedIndex, t.parentNode && t.parentNode.selectedIndex), null
        }
    })), i.support.enctype || (i.propFix.enctype = "encoding"), i.support.checkOn || i.each(["radio", "checkbox"], function () {
        i.valHooks[this] = {
            get: function (n) {
                return n.getAttribute("value") === null ? "on" : n.value
            }
        }
    }), i.each(["radio", "checkbox"], function () {
        i.valHooks[this] = i.extend(i.valHooks[this], {
            set: function (n, t) {
                if (i.isArray(t)) return n.checked = i.inArray(i(n).val(), t) >= 0
            }
        })
    });
    var wt = /^(?:textarea|input|select)$/i,
        pi = /^([^\.]*|)(?:\.(.+)|)$/,
        ho = /(?:^|\s)hover(\.\S+|)\b/,
        co = /^key/,
        so = /^(?:mouse|contextmenu)|click/,
        ui = /^(?:focusinfocus|focusoutblur)$/,
        fi = function (n) {
            return i.event.special.hover ? n : n.replace(ho, "mouseenter$1 mouseleave$1")
        };
    i.event = {
        add: function (n, r, u, f, e) {
            var v, h, a, y, w, o, b, l, p, c, s;
            if (n.nodeType !== 3 && n.nodeType !== 8 && r && u && (v = i._data(n))) {
                for (u.handler && (p = u, u = p.handler, e = p.selector), u.guid || (u.guid = i.guid++), a = v.events, a || (v.events = a = {}), h = v.handle, h || (v.handle = h = function (n) {
                    return typeof i == "undefined" || !! n && i.event.triggered === n.type ? t : i.event.dispatch.apply(h.elem, arguments)
                }, h.elem = n), r = i.trim(fi(r)).split(" "), y = 0; y < r.length; y++) w = pi.exec(r[y]) || [], o = w[1], b = (w[2] || "").split(".").sort(), s = i.event.special[o] || {}, o = (e ? s.delegateType : s.bindType) || o, s = i.event.special[o] || {}, l = i.extend({
                    type: o,
                    origType: w[1],
                    data: f,
                    handler: u,
                    guid: u.guid,
                    selector: e,
                    needsContext: e && i.expr.match.needsContext.test(e),
                    namespace: b.join(".")
                }, p), c = a[o], c || (c = a[o] = [], c.delegateCount = 0, s.setup && s.setup.call(n, f, b, h) !== !1 || (n.addEventListener ? n.addEventListener(o, h, !1) : n.attachEvent && n.attachEvent("on" + o, h))), s.add && (s.add.call(n, l), l.handler.guid || (l.handler.guid = u.guid)), e ? c.splice(c.delegateCount++, 0, l) : c.push(l), i.event.global[o] = !0;
                n = null
            }
        },
        global: {},
        remove: function (n, t, r, u, f) {
            var v, p, e, w, h, b, a, l, c, o, s, y = i.hasData(n) && i._data(n);
            if (y && (l = y.events)) {
                for (t = i.trim(fi(t || "")).split(" "), v = 0; v < t.length; v++) {
                    if (p = pi.exec(t[v]) || [], e = w = p[1], h = p[2], !e) {
                        for (e in l) i.event.remove(n, e + t[v], r, u, !0);
                        continue
                    }
                    for (c = i.event.special[e] || {}, e = (u ? c.delegateType : c.bindType) || e, o = l[e] || [], b = o.length, h = h ? new RegExp("(^|\\.)" + h.split(".").sort().join("\\.(?:.*\\.|)") + "(\\.|$)") : null, a = 0; a < o.length; a++) s = o[a], (f || w === s.origType) && (!r || r.guid === s.guid) && (!h || h.test(s.namespace)) && (!u || u === s.selector || u === "**" && s.selector) && (o.splice(a--, 1), s.selector && o.delegateCount--, c.remove && c.remove.call(n, s));
                    o.length === 0 && b !== o.length && ((!c.teardown || c.teardown.call(n, h, y.handle) === !1) && i.removeEvent(n, e, y.handle), delete l[e])
                }
                i.isEmptyObject(l) && (delete y.handle, i.removeData(n, "events", !0))
            }
        },
        customEvent: {
            getData: !0,
            setData: !0,
            changeData: !0
        },
        trigger: function (u, f, e, o) {
            if (!e || e.nodeType !== 3 && e.nodeType !== 8) {
                var w, d, l, h, c, y, a, v, p, k, s = u.type || u,
                    b = [];
                if (ui.test(s + i.event.triggered)) return;
                if (s.indexOf("!") >= 0 && (s = s.slice(0, -1), d = !0), s.indexOf(".") >= 0 && (b = s.split("."), s = b.shift(), b.sort()), (!e || i.event.customEvent[s]) && !i.event.global[s]) return;
                if (u = typeof u == "object" ? u[i.expando] ? u : new i.Event(s, u) : new i.Event(s), u.type = s, u.isTrigger = !0, u.exclusive = d, u.namespace = b.join("."), u.namespace_re = u.namespace ? new RegExp("(^|\\.)" + b.join("\\.(?:.*\\.|)") + "(\\.|$)") : null, y = s.indexOf(":") < 0 ? "on" + s : "", !e) {
                    w = i.cache;
                    for (l in w) w[l].events && w[l].events[s] && i.event.trigger(u, f, w[l].handle.elem, !0);
                    return
                }
                if (u.result = t, u.target || (u.target = e), f = f != null ? i.makeArray(f) : [], f.unshift(u), a = i.event.special[s] || {}, a.trigger && a.trigger.apply(e, f) === !1) return;
                if (p = [
                    [e, a.bindType || s]
                ], !o && !a.noBubble && !i.isWindow(e)) {
                    for (k = a.delegateType || s, h = ui.test(k + s) ? e : e.parentNode, c = e; h; h = h.parentNode) p.push([h, k]), c = h;
                    c === (e.ownerDocument || r) && p.push([c.defaultView || c.parentWindow || n, k])
                }
                for (l = 0; l < p.length && !u.isPropagationStopped(); l++) h = p[l][0], u.type = p[l][1], v = (i._data(h, "events") || {})[u.type] && i._data(h, "handle"), v && v.apply(h, f), v = y && h[y], v && i.acceptData(h) && v.apply && v.apply(h, f) === !1 && u.preventDefault();
                return u.type = s, !o && !u.isDefaultPrevented() && (!a._default || a._default.apply(e.ownerDocument, f) === !1) && (s !== "click" || !i.nodeName(e, "a")) && i.acceptData(e) && y && e[s] && (s !== "focus" && s !== "blur" || u.target.offsetWidth !== 0) && !i.isWindow(e) && (c = e[y], c && (e[y] = null), i.event.triggered = s, e[s](), i.event.triggered = t, c && (e[y] = c)), u.result
            }
            return
        },
        dispatch: function (r) {
            r = i.event.fix(r || n.event);
            var f, b, e, w, p, h, y, u, s, g, c = (i._data(this, "events") || {})[r.type] || [],
                l = c.delegateCount,
                k = o.call(arguments),
                d = !r.exclusive && !r.namespace,
                v = i.event.special[r.type] || {}, a = [];
            if (k[0] = r, r.delegateTarget = this, !v.preDispatch || v.preDispatch.call(this, r) !== !1) {
                if (l && (!r.button || r.type !== "click")) for (e = r.target; e != this; e = e.parentNode || this) if (e.disabled !== !0 || r.type !== "click") {
                    for (p = {}, y = [], f = 0; f < l; f++) u = c[f], s = u.selector, p[s] === t && (p[s] = u.needsContext ? i(s, this).index(e) >= 0 : i.find(s, this, null, [e]).length), p[s] && y.push(u);
                    y.length && a.push({
                        elem: e,
                        matches: y
                    })
                }
                for (c.length > l && a.push({
                    elem: this,
                    matches: c.slice(l)
                }), f = 0; f < a.length && !r.isPropagationStopped(); f++) for (h = a[f], r.currentTarget = h.elem, b = 0; b < h.matches.length && !r.isImmediatePropagationStopped(); b++) u = h.matches[b], (d || !r.namespace && !u.namespace || r.namespace_re && r.namespace_re.test(u.namespace)) && (r.data = u.data, r.handleObj = u, w = ((i.event.special[u.origType] || {}).handle || u.handler).apply(h.elem, k), w !== t && (r.result = w, w === !1 && (r.preventDefault(), r.stopPropagation())));
                return v.postDispatch && v.postDispatch.call(this, r), r.result
            }
        },
        props: "attrChange attrName relatedNode srcElement altKey bubbles cancelable ctrlKey currentTarget eventPhase metaKey relatedTarget shiftKey target timeStamp view which".split(" "),
        fixHooks: {},
        keyHooks: {
            props: "char charCode key keyCode".split(" "),
            filter: function (n, t) {
                return n.which == null && (n.which = t.charCode != null ? t.charCode : t.keyCode), n
            }
        },
        mouseHooks: {
            props: "button buttons clientX clientY fromElement offsetX offsetY pageX pageY screenX screenY toElement".split(" "),
            filter: function (n, i) {
                var s, f, u, e = i.button,
                    o = i.fromElement;
                return n.pageX == null && i.clientX != null && (s = n.target.ownerDocument || r, f = s.documentElement, u = s.body, n.pageX = i.clientX + (f && f.scrollLeft || u && u.scrollLeft || 0) - (f && f.clientLeft || u && u.clientLeft || 0), n.pageY = i.clientY + (f && f.scrollTop || u && u.scrollTop || 0) - (f && f.clientTop || u && u.clientTop || 0)), !n.relatedTarget && o && (n.relatedTarget = o === n.target ? i.toElement : o), !n.which && e !== t && (n.which = e & 1 ? 1 : e & 2 ? 3 : e & 4 ? 2 : 0), n
            }
        },
        fix: function (n) {
            if (n[i.expando]) return n;
            var f, e, t = n,
                u = i.event.fixHooks[n.type] || {}, o = u.props ? this.props.concat(u.props) : this.props;
            for (n = i.Event(t), f = o.length; f;) e = o[--f], n[e] = t[e];
            return n.target || (n.target = t.srcElement || r), n.target.nodeType === 3 && (n.target = n.target.parentNode), n.metaKey = !! n.metaKey, u.filter ? u.filter(n, t) : n
        },
        special: {
            load: {
                noBubble: !0
            },
            focus: {
                delegateType: "focusin"
            },
            blur: {
                delegateType: "focusout"
            },
            beforeunload: {
                setup: function (n, t, r) {
                    i.isWindow(this) && (this.onbeforeunload = r)
                },
                teardown: function (n, t) {
                    this.onbeforeunload === t && (this.onbeforeunload = null)
                }
            }
        },
        simulate: function (n, t, r, u) {
            var f = i.extend(new i.Event, r, {
                type: n,
                isSimulated: !0,
                originalEvent: {}
            });
            u ? i.event.trigger(f, null, t) : i.event.dispatch.call(t, f), f.isDefaultPrevented() && r.preventDefault()
        }
    }, i.event.handle = i.event.dispatch, i.removeEvent = r.removeEventListener ? function (n, t, i) {
        n.removeEventListener && n.removeEventListener(t, i, !1)
    } : function (n, t, i) {
        var r = "on" + t;
        n.detachEvent && (typeof n[r] == "undefined" && (n[r] = null), n.detachEvent(r, i))
    }, i.Event = function (n, t) {
        if (!(this instanceof i.Event)) return new i.Event(n, t);
        n && n.type ? (this.originalEvent = n, this.type = n.type, this.isDefaultPrevented = n.defaultPrevented || n.returnValue === !1 || n.getPreventDefault && n.getPreventDefault() ? ct : y) : this.type = n, t && i.extend(this, t), this.timeStamp = n && n.timeStamp || i.now(), this[i.expando] = !0
    }, i.Event.prototype = {
        preventDefault: function () {
            this.isDefaultPrevented = ct;
            var n = this.originalEvent;
            n && (n.preventDefault ? n.preventDefault() : n.returnValue = !1)
        },
        stopPropagation: function () {
            this.isPropagationStopped = ct;
            var n = this.originalEvent;
            n && (n.stopPropagation && n.stopPropagation(), n.cancelBubble = !0)
        },
        stopImmediatePropagation: function () {
            this.isImmediatePropagationStopped = ct, this.stopPropagation()
        },
        isDefaultPrevented: y,
        isPropagationStopped: y,
        isImmediatePropagationStopped: y
    }, i.each({
        mouseenter: "mouseover",
        mouseleave: "mouseout"
    }, function (n, t) {
        i.event.special[n] = {
            delegateType: t,
            bindType: t,
            handle: function (n) {
                var f, e = this,
                    r = n.relatedTarget,
                    u = n.handleObj,
                    o = u.selector;
                return r && (r === e || i.contains(e, r)) || (n.type = u.origType, f = u.handler.apply(this, arguments), n.type = t), f
            }
        }
    }), i.support.submitBubbles || (i.event.special.submit = {
        setup: function () {
            if (i.nodeName(this, "form")) return !1;
            i.event.add(this, "click._submit keypress._submit", function (n) {
                var u = n.target,
                    r = i.nodeName(u, "input") || i.nodeName(u, "button") ? u.form : t;
                r && !i._data(r, "_submit_attached") && (i.event.add(r, "submit._submit", function (n) {
                    n._submit_bubble = !0
                }), i._data(r, "_submit_attached", !0))
            })
        },
        postDispatch: function (n) {
            n._submit_bubble && (delete n._submit_bubble, this.parentNode && !n.isTrigger && i.event.simulate("submit", this.parentNode, n, !0))
        },
        teardown: function () {
            if (i.nodeName(this, "form")) return !1;
            i.event.remove(this, "._submit")
        }
    }), i.support.changeBubbles || (i.event.special.change = {
        setup: function () {
            if (wt.test(this.nodeName)) return (this.type === "checkbox" || this.type === "radio") && (i.event.add(this, "propertychange._change", function (n) {
                n.originalEvent.propertyName === "checked" && (this._just_changed = !0)
            }), i.event.add(this, "click._change", function (n) {
                this._just_changed && !n.isTrigger && (this._just_changed = !1), i.event.simulate("change", this, n, !0)
            })), !1;
            i.event.add(this, "beforeactivate._change", function (n) {
                var t = n.target;
                wt.test(t.nodeName) && !i._data(t, "_change_attached") && (i.event.add(t, "change._change", function (n) {
                    this.parentNode && !n.isSimulated && !n.isTrigger && i.event.simulate("change", this.parentNode, n, !0)
                }), i._data(t, "_change_attached", !0))
            })
        },
        handle: function (n) {
            var t = n.target;
            if (this !== t || n.isSimulated || n.isTrigger || t.type !== "radio" && t.type !== "checkbox") return n.handleObj.handler.apply(this, arguments)
        },
        teardown: function () {
            return i.event.remove(this, "._change"), !wt.test(this.nodeName)
        }
    }), i.support.focusinBubbles || i.each({
        focus: "focusin",
        blur: "focusout"
    }, function (n, t) {
        var f = 0,
            u = function (n) {
                i.event.simulate(t, n.target, i.event.fix(n), !0)
            };
        i.event.special[t] = {
            setup: function () {
                f++ == 0 && r.addEventListener(n, u, !0)
            },
            teardown: function () {
                --f == 0 && r.removeEventListener(n, u, !0)
            }
        }
    }), i.fn.extend({
        on: function (n, r, u, f, e) {
            var o, s;
            if (typeof n == "object") {
                typeof r != "string" && (u = u || r, r = t);
                for (s in n) this.on(s, r, u, n[s], e);
                return this
            }
            if (u == null && f == null ? (f = r, u = r = t) : f == null && (typeof r == "string" ? (f = u, u = t) : (f = u, u = r, r = t)), f === !1) f = y;
            else if (!f) return this;
            return e === 1 && (o = f, f = function (n) {
                return i().off(n), o.apply(this, arguments)
            }, f.guid = o.guid || (o.guid = i.guid++)), this.each(function () {
                i.event.add(this, n, f, u, r)
            })
        },
        one: function (n, t, i, r) {
            return this.on(n, t, i, r, 1)
        },
        off: function (n, r, u) {
            var f, e;
            if (n && n.preventDefault && n.handleObj) return f = n.handleObj, i(n.delegateTarget).off(f.namespace ? f.origType + "." + f.namespace : f.origType, f.selector, f.handler), this;
            if (typeof n == "object") {
                for (e in n) this.off(e, r, n[e]);
                return this
            }
            return (r === !1 || typeof r == "function") && (u = r, r = t), u === !1 && (u = y), this.each(function () {
                i.event.remove(this, n, u, r)
            })
        },
        bind: function (n, t, i) {
            return this.on(n, null, t, i)
        },
        unbind: function (n, t) {
            return this.off(n, null, t)
        },
        live: function (n, t, r) {
            return i(this.context).on(n, this.selector, t, r), this
        },
        die: function (n, t) {
            return i(this.context).off(n, this.selector || "**", t), this
        },
        delegate: function (n, t, i, r) {
            return this.on(t, n, i, r)
        },
        undelegate: function (n, t, i) {
            return arguments.length === 1 ? this.off(n, "**") : this.off(t, n || "**", i)
        },
        trigger: function (n, t) {
            return this.each(function () {
                i.event.trigger(n, t, this)
            })
        },
        triggerHandler: function (n, t) {
            if (this[0]) return i.event.trigger(n, t, this[0], !0)
        },
        toggle: function (n) {
            var r = arguments,
                f = n.guid || i.guid++,
                t = 0,
                u = function (u) {
                    var f = (i._data(this, "lastToggle" + n.guid) || 0) % t;
                    return i._data(this, "lastToggle" + n.guid, f + 1), u.preventDefault(), r[f].apply(this, arguments) || !1
                };
            for (u.guid = f; t < r.length;) r[t++].guid = f;
            return this.click(u)
        },
        hover: function (n, t) {
            return this.mouseenter(n).mouseleave(t || n)
        }
    }), i.each("blur focus focusin focusout load resize scroll unload click dblclick mousedown mouseup mousemove mouseover mouseout mouseenter mouseleave change select submit keydown keypress keyup error contextmenu".split(" "), function (n, t) {
        i.fn[t] = function (n, i) {
            return i == null && (i = n, n = null), arguments.length > 0 ? this.on(t, null, n, i) : this.trigger(t)
        }, co.test(t) && (i.event.fixHooks[t] = i.event.keyHooks), so.test(t) && (i.event.fixHooks[t] = i.event.mouseHooks)
    }),
    function (n, t) {
        function r(n, t, i, r) {
            i = i || [], t = t || h;
            var e, u, s, f, o = t.nodeType;
            if (!n || typeof n != "string") return i;
            if (o !== 1 && o !== 9) return [];
            if (s = et(t), !s && !r && (e = tr.exec(n))) if (f = e[1]) {
                if (o === 9) {
                    if (u = t.getElementById(f), !u || !u.parentNode) return i;
                    if (u.id === f) return i.push(u), i
                } else if (t.ownerDocument && (u = t.ownerDocument.getElementById(f)) && ti(t, u) && u.id === f) return i.push(u), i
            } else {
                if (e[2]) return w.apply(i, p.call(t.getElementsByTagName(n), 0)), i;
                if ((f = e[3]) && gt && t.getElementsByClassName) return w.apply(i, p.call(t.getElementsByClassName(f), 0)), i
            }
            return ct(n.replace(tt, "$1"), t, i, r, s)
        }
        function k(n) {
            return function (t) {
                var i = t.nodeName.toLowerCase();
                return i === "input" && t.type === n
            }
        }
        function oi(n) {
            return function (t) {
                var i = t.nodeName.toLowerCase();
                return (i === "input" || i === "button") && t.type === n
            }
        }
        function y(n) {
            return s(function (t) {
                return t = +t, s(function (i, r) {
                    for (var u, e = n([], i.length, t), f = e.length; f--;) i[u = e[f]] && (i[u] = !(r[u] = i[u]))
                })
            })
        }
        function rt(n, t, i) {
            if (n === t) return i;
            for (var r = n.nextSibling; r;) {
                if (r === t) return -1;
                r = r.nextSibling
            }
            return 1
        }
        function d(n, t) {
            var o, f, l, s, i, c, h, a = fi[e][n + " "];
            if (a) return t ? 0 : a.slice(0);
            for (i = n, c = [], h = u.preFilter; i;) {
                (!o || (f = nr.exec(i))) && (f && (i = i.slice(f[0].length) || i), c.push(l = [])), o = !1, (f = rr.exec(i)) && (l.push(o = new si(f.shift())), i = i.slice(o.length), o.type = f[0].replace(tt, " "));
                for (s in u.filter)(f = g[s].exec(i)) && (!h[s] || (f = h[s](f))) && (l.push(o = new si(f.shift())), i = i.slice(o.length), o.type = s, o.matches = f);
                if (!o) break
            }
            return t ? i.length : i ? r.error(n) : fi(n, c).slice(0)
        }
        function bt(n, t, i) {
            var u = t.dir,
                r = i && t.dir === "parentNode",
                f = gi++;
            return t.first ? function (t, i, f) {
                while (t = t[u]) if (r || t.nodeType === 1) return n(t, i, f)
            } : function (t, i, o) {
                if (o) {
                    while (t = t[u]) if ((r || t.nodeType === 1) && n(t, i, o)) return t
                } else for (var s, c = it + " " + f + " ", h = c + ot; t = t[u];) if (r || t.nodeType === 1) {
                    if ((s = t[e]) === h) return t.sizset;
                    if (typeof s == "string" && s.indexOf(c) === 0) {
                        if (t.sizset) return t
                    } else {
                        if (t[e] = h, n(t, i, o)) return t.sizset = !0, t;
                        t.sizset = !1
                    }
                }
            }
        }
        function wt(n) {
            return n.length > 1 ? function (t, i, r) {
                for (var u = n.length; u--;) if (!n[u](t, i, r)) return !1;
                return !0
            } : n[0]
        }
        function ft(n, t, i, r, u) {
            for (var e, o = [], f = 0, h = n.length, s = t != null; f < h; f++)(e = n[f]) && (!i || i(e, r, u)) && (o.push(e), s && t.push(f));
            return o
        }
        function at(n, t, i, r, u, f) {
            return r && !r[e] && (r = at(r)), u && !u[e] && (u = at(u, f)), s(function (f, e, o, s) {
                var l, c, a, p = [],
                    y = [],
                    b = e.length,
                    k = f || di(t || "*", o.nodeType ? [o] : o, []),
                    v = n && (f || !t) ? ft(k, p, n, o, s) : k,
                    h = i ? u || (f ? n : b || r) ? [] : e : v;
                if (i && i(v, h, o, s), r) for (l = ft(h, y), r(l, [], o, s), c = l.length; c--;)(a = l[c]) && (h[y[c]] = !(v[y[c]] = a));
                if (f) {
                    if (u || n) {
                        if (u) {
                            for (l = [], c = h.length; c--;)(a = h[c]) && l.push(v[c] = a);
                            u(null, h = [], l, s)
                        }
                        for (c = h.length; c--;)(a = h[c]) && (l = u ? ht.call(f, a) : p[c]) > -1 && (f[l] = !(e[l] = a))
                    }
                } else h = ft(h === e ? h.splice(b, h.length) : h), u ? u(null, e, h, s) : w.apply(e, h)
            })
        }
        function lt(n) {
            for (var h, r, i, o = n.length, s = u.relative[n[0].type], c = s || u.relative[" "], t = s ? 1 : 0, l = bt(function (n) {
                return n === h
            }, c, !0), a = bt(function (n) {
                return ht.call(h, n) > -1
            }, c, !0), f = [function (n, t, i) {
                return !s && (i || t !== nt) || ((h = t).nodeType ? l(n, t, i) : a(n, t, i))
            }]; t < o; t++) if (r = u.relative[n[t].type]) f = [bt(wt(f), r)];
            else {
                if (r = u.filter[n[t].type].apply(null, n[t].matches), r[e]) {
                    for (i = ++t; i < o; i++) if (u.relative[n[i].type]) break;
                    return at(t > 1 && wt(f), t > 1 && n.slice(0, t - 1).join("").replace(tt, "$1"), r, t < i && lt(n.slice(t, i)), i < o && lt(n = n.slice(i)), i < o && n.join(""))
                }
                f.push(r)
            }
            return wt(f)
        }
        function bi(n, t) {
            var f = t.length > 0,
                e = n.length > 0,
                i = function (o, s, c, l, a) {
                    var p, b, d, y = [],
                        k = 0,
                        v = "0",
                        tt = o && [],
                        g = a != null,
                        ut = nt,
                        et = o || e && u.find.TAG("*", a && s.parentNode || s),
                        rt = it += ut == null ? 1 : Math.E;
                    for (g && (nt = s !== h && s, ot = i.el);
                    (p = et[v]) != null; v++) {
                        if (e && p) {
                            for (b = 0; d = n[b]; b++) if (d(p, s, c)) {
                                l.push(p);
                                break
                            }
                            g && (it = rt, ot = ++i.el)
                        }
                        f && ((p = !d && p) && k--, o && tt.push(p))
                    }
                    if (k += v, f && v !== k) {
                        for (b = 0; d = t[b]; b++) d(tt, y, s, c);
                        if (o) {
                            if (k > 0) while (v--)!tt[v] && !y[v] && (y[v] = ki.call(l));
                            y = ft(y)
                        }
                        w.apply(l, y), g && !o && y.length > 0 && k + t.length > 1 && r.uniqueSort(l)
                    }
                    return g && (it = rt, nt = ut), tt
                };
            return i.el = 0, f ? s(i) : i
        }
        function di(n, t, i) {
            for (var u = 0, f = t.length; u < f; u++) r(n, t[u], i);
            return i
        }
        function ct(n, t, i, r, f) {
            var h, e, s, l, c, o = d(n),
                a = o.length;
            if (!r && o.length === 1) {
                if (e = o[0] = o[0].slice(0), e.length > 2 && (s = e[0]).type === "ID" && t.nodeType === 9 && !f && u.relative[e[1].type]) {
                    if (t = u.find.ID(s.matches[0].replace(v, ""), t, f)[0], !t) return i;
                    n = n.slice(e.shift().length)
                }
                for (h = g.POS.test(n) ? -1 : e.length - 1; h >= 0; h--) {
                    if (s = e[h], u.relative[l = s.type]) break;
                    if ((c = u.find[l]) && (r = c(s.matches[0].replace(v, ""), vt.test(e[0].type) && t.parentNode || t, f))) {
                        if (e.splice(h, 1), n = r.length && e.join(""), !n) return w.apply(i, p.call(r, 0)), i;
                        break
                    }
                }
            }
            return kt(n, o)(r, t, f, i, vt.test(n)), i
        }
        function ei() {}
        var ot, pt, u, ut, et, ti, kt, dt, b, nt, ii = !0,
            c = "undefined",
            e = ("sizcache" + Math.random()).replace(".", ""),
            si = String,
            h = n.document,
            o = h.documentElement,
            it = 0,
            gi = 0,
            ki = [].pop,
            w = [].push,
            p = [].slice,
            ht = [].indexOf || function (n) {
                for (var t = 0, i = this.length; t < i; t++) if (this[t] === n) return t;
                return -1
            }, s = function (n, t) {
                return n[e] = t == null || t, n
            }, st = function () {
                var n = {}, t = [];
                return s(function (i, r) {
                    return t.push(i) > u.cacheLength && delete n[t.shift()], n[i + " "] = r
                }, n)
            }, ui = st(),
            fi = st(),
            hi = st(),
            f = "[\\x20\\t\\r\\n\\f]",
            a = "(?:\\\\.|[-\\w]|[^\\x00-\\xa0])+",
            wi = a.replace("w", "w#"),
            ir = "([*^$|!~]?=)",
            ni = "\\[" + f + "*(" + a + ")" + f + "*(?:" + ir + f + "*(?:(['\"])((?:\\\\.|[^\\\\])*?)\\3|(" + wi + ")|)|)" + f + "*\\]",
            yt = ":(" + a + ")(?:\\((?:(['\"])((?:\\\\.|[^\\\\])*?)\\2|([^()[\\]]*|(?:(?:" + ni + ")|[^:]|\\\\.)*|.*))\\)|)",
            ri = ":(even|odd|eq|gt|lt|nth|first|last)(?:\\(" + f + "*((?:-\\d)?\\d*)" + f + "*\\)|)(?=[^-]|$)",
            tt = new RegExp("^" + f + "+|((?:^|[^\\\\])(?:\\\\.)*)" + f + "+$", "g"),
            nr = new RegExp("^" + f + "*," + f + "*"),
            rr = new RegExp("^" + f + "*([\\x20\\t\\r\\n\\f>+~])" + f + "*"),
            ur = new RegExp(yt),
            tr = /^(?:#([\w\-]+)|(\w+)|\.([\w\-]+))$/,
            or = /^:not/,
            vt = /[\x20\t\r\n\f]*[+~]/,
            er = /:not\($/,
            ai = /h\d/i,
            ci = /input|select|textarea|button/i,
            v = /\\(?!\\)/g,
            g = {
                ID: new RegExp("^#(" + a + ")"),
                CLASS: new RegExp("^\\.(" + a + ")"),
                NAME: new RegExp("^\\[name=['\"]?(" + a + ")['\"]?\\]"),
                TAG: new RegExp("^(" + a.replace("w", "w*") + ")"),
                ATTR: new RegExp("^" + ni),
                PSEUDO: new RegExp("^" + yt),
                POS: new RegExp(ri, "i"),
                CHILD: new RegExp("^:(only|nth|first|last)-child(?:\\(" + f + "*(even|odd|(([+-]|)(\\d*)n|)" + f + "*(?:([+-]|)" + f + "*(\\d+)|))" + f + "*\\)|)", "i"),
                needsContext: new RegExp("^" + f + "*[>+~]|" + ri, "i")
            }, l = function (n) {
                var t = h.createElement("div");
                try {
                    return n(t)
                } catch (i) {
                    return !1
                } finally {
                    t = null
                }
            }, yi = l(function (n) {
                return n.appendChild(h.createComment("")), !n.getElementsByTagName("*").length
            }),
            li = l(function (n) {
                return n.innerHTML = "<a href='#'></a>", n.firstChild && typeof n.firstChild.getAttribute !== c && n.firstChild.getAttribute("href") === "#"
            }),
            pi = l(function (n) {
                n.innerHTML = "<select></select>";
                var t = typeof n.lastChild.getAttribute("multiple");
                return t !== "boolean" && t !== "string"
            }),
            gt = l(function (n) {
                return n.innerHTML = "<div class='hidden e'></div><div class='hidden'></div>", !n.getElementsByClassName || !n.getElementsByClassName("e").length ? !1 : (n.lastChild.className = "e", n.getElementsByClassName("e").length === 2)
            }),
            vi = l(function (n) {
                n.id = e + 0, n.innerHTML = "<a name='" + e + "'></a><div name='" + e + "'></div>", o.insertBefore(n, o.firstChild);
                var t = h.getElementsByName && h.getElementsByName(e).length === 2 + h.getElementsByName(e + 0).length;
                return pt = !h.getElementById(e), o.removeChild(n), t
            });
        try {
            p.call(o.childNodes, 0)[0].nodeType
        } catch (fr) {
            p = function (n) {
                for (var i, t = []; i = this[n]; n++) t.push(i);
                return t
            }
        }
        r.matches = function (n, t) {
            return r(n, null, null, t)
        }, r.matchesSelector = function (n, t) {
            return r(t, null, null, [n]).length > 0
        }, ut = r.getText = function (n) {
            var r, i = "",
                u = 0,
                t = n.nodeType;
            if (t) {
                if (t === 1 || t === 9 || t === 11) {
                    if (typeof n.textContent == "string") return n.textContent;
                    for (n = n.firstChild; n; n = n.nextSibling) i += ut(n)
                } else if (t === 3 || t === 4) return n.nodeValue
            } else for (; r = n[u]; u++) i += ut(r);
            return i
        }, et = r.isXML = function (n) {
            var t = n && (n.ownerDocument || n).documentElement;
            return t ? t.nodeName !== "HTML" : !1
        }, ti = r.contains = o.contains ? function (n, t) {
            var r = n.nodeType === 9 ? n.documentElement : n,
                i = t && t.parentNode;
            return n === i || !! (i && i.nodeType === 1 && r.contains && r.contains(i))
        } : o.compareDocumentPosition ? function (n, t) {
            return t && !! (n.compareDocumentPosition(t) & 16)
        } : function (n, t) {
            while (t = t.parentNode) if (t === n) return !0;
            return !1
        }, r.attr = function (n, t) {
            var i, r = et(n);
            return r || (t = t.toLowerCase()), (i = u.attrHandle[t]) ? i(n) : r || pi ? n.getAttribute(t) : (i = n.getAttributeNode(t), i ? typeof n[t] == "boolean" ? n[t] ? t : null : i.specified ? i.value : null : null)
        }, u = r.selectors = {
            cacheLength: 50,
            createPseudo: s,
            match: g,
            attrHandle: li ? {} : {
                href: function (n) {
                    return n.getAttribute("href", 2)
                },
                type: function (n) {
                    return n.getAttribute("type")
                }
            },
            find: {
                ID: pt ? function (n, t, i) {
                    if (typeof t.getElementById !== c && !i) {
                        var r = t.getElementById(n);
                        return r && r.parentNode ? [r] : []
                    }
                } : function (n, i, r) {
                    if (typeof i.getElementById !== c && !r) {
                        var u = i.getElementById(n);
                        return u ? u.id === n || typeof u.getAttributeNode !== c && u.getAttributeNode("id").value === n ? [u] : t : []
                    }
                },
                TAG: yi ? function (n, t) {
                    if (typeof t.getElementsByTagName !== c) return t.getElementsByTagName(n)
                } : function (n, t) {
                    var f = t.getElementsByTagName(n),
                        r, i, u;
                    if (n === "*") {
                        for (i = [], u = 0; r = f[u]; u++) r.nodeType === 1 && i.push(r);
                        return i
                    }
                    return f
                },
                NAME: vi && function (n, t) {
                    if (typeof t.getElementsByName !== c) return t.getElementsByName(name)
                },
                CLASS: gt && function (n, t, i) {
                    if (typeof t.getElementsByClassName !== c && !i) return t.getElementsByClassName(n)
                }
            },
            relative: {
                ">": {
                    dir: "parentNode",
                    first: !0
                },
                " ": {
                    dir: "parentNode"
                },
                "+": {
                    dir: "previousSibling",
                    first: !0
                },
                "~": {
                    dir: "previousSibling"
                }
            },
            preFilter: {
                ATTR: function (n) {
                    return n[1] = n[1].replace(v, ""), n[3] = (n[4] || n[5] || "").replace(v, ""), n[2] === "~=" && (n[3] = " " + n[3] + " "), n.slice(0, 4)
                },
                CHILD: function (n) {
                    return n[1] = n[1].toLowerCase(), n[1] === "nth" ? (n[2] || r.error(n[0]), n[3] = +(n[3] ? n[4] + (n[5] || 1) : 2 * (n[2] === "even" || n[2] === "odd")), n[4] = +(n[6] + n[7] || n[2] === "odd")) : n[2] && r.error(n[0]), n
                },
                PSEUDO: function (n) {
                    var t, i;
                    return g.CHILD.test(n[0]) ? null : (n[3] ? n[2] = n[3] : (t = n[4]) && (ur.test(t) && (i = d(t, !0)) && (i = t.indexOf(")", t.length - i) - t.length) && (t = t.slice(0, i), n[0] = n[0].slice(0, i)), n[2] = t), n.slice(0, 3))
                }
            },
            filter: {
                ID: pt ? function (n) {
                    return n = n.replace(v, ""),
                    function (t) {
                        return t.getAttribute("id") === n
                    }
                } : function (n) {
                    return n = n.replace(v, ""),
                    function (t) {
                        var i = typeof t.getAttributeNode !== c && t.getAttributeNode("id");
                        return i && i.value === n
                    }
                },
                TAG: function (n) {
                    return n === "*" ? function () {
                        return !0
                    } : (n = n.replace(v, "").toLowerCase(), function (t) {
                        return t.nodeName && t.nodeName.toLowerCase() === n
                    })
                },
                CLASS: function (n) {
                    var t = ui[e][n + " "];
                    return t || (t = new RegExp("(^|" + f + ")" + n + "(" + f + "|$)")) && ui(n, function (n) {
                        return t.test(n.className || typeof n.getAttribute !== c && n.getAttribute("class") || "")
                    })
                },
                ATTR: function (n, t, i) {
                    return function (u) {
                        var e = r.attr(u, n);
                        return e == null ? t === "!=" : t ? (e += "", t === "=" ? e === i : t === "!=" ? e !== i : t === "^=" ? i && e.indexOf(i) === 0 : t === "*=" ? i && e.indexOf(i) > -1 : t === "$=" ? i && e.substr(e.length - i.length) === i : t === "~=" ? (" " + e + " ").indexOf(i) > -1 : t === "|=" ? e === i || e.substr(0, i.length + 1) === i + "-" : !1) : !0
                    }
                },
                CHILD: function (n, t, i, r) {
                    return n === "nth" ? function (n) {
                        var u, t, f = n.parentNode;
                        if (i === 1 && r === 0) return !0;
                        if (f) for (t = 0, u = f.firstChild; u; u = u.nextSibling) if (u.nodeType === 1 && (t++, n === u)) break;
                        return t -= r, t === i || t % i == 0 && t / i >= 0
                    } : function (t) {
                        var i = t;
                        switch (n) {
                            case "only":
                            case "first":
                                while (i = i.previousSibling) if (i.nodeType === 1) return !1;
                                if (n === "first") return !0;
                                i = t;
                            case "last":
                                while (i = i.nextSibling) if (i.nodeType === 1) return !1;
                                return !0
                        }
                    }
                },
                PSEUDO: function (n, t) {
                    var f, i = u.pseudos[n] || u.setFilters[n.toLowerCase()] || r.error("unsupported pseudo: " + n);
                    return i[e] ? i(t) : i.length > 1 ? (f = [n, n, "", t], u.setFilters.hasOwnProperty(n.toLowerCase()) ? s(function (n, r) {
                        for (var e, f = i(n, t), u = f.length; u--;) e = ht.call(n, f[u]), n[e] = !(r[e] = f[u])
                    }) : function (n) {
                        return i(n, 0, f)
                    }) : i
                }
            },
            pseudos: {
                not: s(function (n) {
                    var i = [],
                        r = [],
                        t = kt(n.replace(tt, "$1"));
                    return t[e] ? s(function (n, i, r, u) {
                        for (var e, o = t(n, null, u, []), f = n.length; f--;)(e = o[f]) && (n[f] = !(i[f] = e))
                    }) : function (n, u, f) {
                        return i[0] = n, t(i, null, f, r), !r.pop()
                    }
                }),
                has: s(function (n) {
                    return function (t) {
                        return r(n, t).length > 0
                    }
                }),
                contains: s(function (n) {
                    return function (t) {
                        return (t.textContent || t.innerText || ut(t)).indexOf(n) > -1
                    }
                }),
                enabled: function (n) {
                    return n.disabled === !1
                },
                disabled: function (n) {
                    return n.disabled === !0
                },
                checked: function (n) {
                    var t = n.nodeName.toLowerCase();
                    return t === "input" && !! n.checked || t === "option" && !! n.selected
                },
                selected: function (n) {
                    return n.parentNode && n.parentNode.selectedIndex, n.selected === !0
                },
                parent: function (n) {
                    return !u.pseudos.empty(n)
                },
                empty: function (n) {
                    var t;
                    for (n = n.firstChild; n;) {
                        if (n.nodeName > "@" || (t = n.nodeType) === 3 || t === 4) return !1;
                        n = n.nextSibling
                    }
                    return !0
                },
                header: function (n) {
                    return ai.test(n.nodeName)
                },
                text: function (n) {
                    var i, t;
                    return n.nodeName.toLowerCase() === "input" && (i = n.type) === "text" && ((t = n.getAttribute("type")) == null || t.toLowerCase() === i)
                },
                radio: k("radio"),
                checkbox: k("checkbox"),
                file: k("file"),
                password: k("password"),
                image: k("image"),
                submit: oi("submit"),
                reset: oi("reset"),
                button: function (n) {
                    var t = n.nodeName.toLowerCase();
                    return t === "input" && n.type === "button" || t === "button"
                },
                input: function (n) {
                    return ci.test(n.nodeName)
                },
                focus: function (n) {
                    var t = n.ownerDocument;
                    return n === t.activeElement && (!t.hasFocus || t.hasFocus()) && !! (n.type || n.href || ~n.tabIndex)
                },
                active: function (n) {
                    return n === n.ownerDocument.activeElement
                },
                first: y(function () {
                    return [0]
                }),
                last: y(function (n, t) {
                    return [t - 1]
                }),
                eq: y(function (n, t, i) {
                    return [i < 0 ? i + t : i]
                }),
                even: y(function (n, t) {
                    for (var i = 0; i < t; i += 2) n.push(i);
                    return n
                }),
                odd: y(function (n, t) {
                    for (var i = 1; i < t; i += 2) n.push(i);
                    return n
                }),
                lt: y(function (n, t, i) {
                    for (var r = i < 0 ? i + t : i; --r >= 0;) n.push(r);
                    return n
                }),
                gt: y(function (n, t, i) {
                    for (var r = i < 0 ? i + t : i; ++r < t;) n.push(r);
                    return n
                })
            }
        }, dt = o.compareDocumentPosition ? function (n, t) {
            return n === t ? (b = !0, 0) : (!n.compareDocumentPosition || !t.compareDocumentPosition ? n.compareDocumentPosition : n.compareDocumentPosition(t) & 4) ? -1 : 1
        } : function (n, t) {
            var i;
            if (n === t) return b = !0, 0;
            if (n.sourceIndex && t.sourceIndex) return n.sourceIndex - t.sourceIndex;
            var o, h, f = [],
                u = [],
                s = n.parentNode,
                e = t.parentNode,
                r = s;
            if (s === e) return rt(n, t);
            if (!s) return -1;
            if (!e) return 1;
            while (r) f.unshift(r), r = r.parentNode;
            for (r = e; r;) u.unshift(r), r = r.parentNode;
            for (o = f.length, h = u.length, i = 0; i < o && i < h; i++) if (f[i] !== u[i]) return rt(f[i], u[i]);
            return i === o ? rt(n, u[i], -1) : rt(f[i], t, 1)
        }, [0, 0].sort(dt), ii = !b, r.uniqueSort = function (n) {
            var u, r = [],
                t = 1,
                i = 0;
            if (b = ii, n.sort(dt), b) {
                for (; u = n[t]; t++) u === n[t - 1] && (i = r.push(t));
                while (i--) n.splice(r[i], 1)
            }
            return n
        }, r.error = function (n) {
            throw new Error("Syntax error, unrecognized expression: " + n);
        }, kt = r.compile = function (n, t) {
            var r, u = [],
                f = [],
                i = hi[e][n + " "];
            if (!i) {
                for (t || (t = d(n)), r = t.length; r--;) i = lt(t[r]), i[e] ? u.push(i) : f.push(i);
                i = hi(n, bi(f, u))
            }
            return i
        }, h.querySelectorAll && function () {
            var u, h = ct,
                c = /'|\\/g,
                s = /\=[\x20\t\r\n\f]*([^'"\]]*)[\x20\t\r\n\f]*\]/g,
                n = [":focus"],
                i = [":active"],
                t = o.matchesSelector || o.mozMatchesSelector || o.webkitMatchesSelector || o.oMatchesSelector || o.msMatchesSelector;
            l(function (t) {
                t.innerHTML = "<select><option selected=''></option></select>", t.querySelectorAll("[selected]").length || n.push("\\[" + f + "*(?:checked|disabled|ismap|multiple|readonly|selected|value)"), t.querySelectorAll(":checked").length || n.push(":checked")
            }), l(function (t) {
                t.innerHTML = "<p test=''></p>", t.querySelectorAll("[test^='']").length && n.push("[*^$]=" + f + "*(?:\"\"|'')"), t.innerHTML = "<input type='hidden'/>", t.querySelectorAll(":enabled").length || n.push(":enabled", ":disabled")
            }), n = new RegExp(n.join("|")), ct = function (t, i, r, u, f) {
                if (!u && !f && !n.test(t)) {
                    var s, l, v = !0,
                        o = e,
                        y = i,
                        a = i.nodeType === 9 && t;
                    if (i.nodeType === 1 && i.nodeName.toLowerCase() !== "object") {
                        for (s = d(t), (v = i.getAttribute("id")) ? o = v.replace(c, "\\$&") : i.setAttribute("id", o), o = "[id='" + o + "'] ", l = s.length; l--;) s[l] = o + s[l].join("");
                        y = vt.test(t) && i.parentNode || i, a = s.join(",")
                    }
                    if (a) try {
                        return w.apply(r, p.call(y.querySelectorAll(a), 0)), r
                    } catch (b) {} finally {
                        v || i.removeAttribute("id")
                    }
                }
                return h(t, i, r, u, f)
            }, t && (l(function (n) {
                u = t.call(n, "div");
                try {
                    t.call(n, "[test!='']:sizzle"), i.push("!=", yt)
                } catch (r) {}
            }), i = new RegExp(i.join("|")), r.matchesSelector = function (f, e) {
                if (e = e.replace(s, "='$1']"), !et(f) && !i.test(e) && !n.test(e)) try {
                    var o = t.call(f, e);
                    if (o || u || f.document && f.document.nodeType !== 11) return o
                } catch (h) {}
                return r(e, null, null, [f]).length > 0
            })
        }(), u.pseudos.nth = u.pseudos.eq, u.filters = ei.prototype = u.pseudos, u.setFilters = new ei, r.attr = i.attr, i.find = r, i.expr = r.selectors, i.expr[":"] = i.expr.pseudos, i.unique = r.uniqueSort, i.text = r.getText, i.isXMLDoc = r.isXML, i.contains = r.contains
    }(n);
    var eo = /Until$/,
        oo = /^(?:parents|prev(?:Until|All))/,
        yo = /^.[^:#\[\.,]*$/,
        ci = i.expr.match.needsContext,
        po = {
            children: !0,
            contents: !0,
            next: !0,
            prev: !0
        };
    i.fn.extend({
        find: function (n) {
            var t, e, o, u, f, r, s = this;
            if (typeof n != "string") return i(n).filter(function () {
                for (t = 0, e = s.length; t < e; t++) if (i.contains(s[t], this)) return !0
            });
            for (r = this.pushStack("", "find", n), t = 0, e = this.length; t < e; t++) if (o = r.length, i.find(n, this[t], r), t > 0) for (u = o; u < r.length; u++) for (f = 0; f < o; f++) if (r[f] === r[u]) {
                r.splice(u--, 1);
                break
            }
            return r
        },
        has: function (n) {
            var t, r = i(n, this),
                u = r.length;
            return this.filter(function () {
                for (t = 0; t < u; t++) if (i.contains(this, r[t])) return !0
            })
        },
        not: function (n) {
            return this.pushStack(yr(this, n, !1), "not", n)
        },
        filter: function (n) {
            return this.pushStack(yr(this, n, !0), "filter", n)
        },
        is: function (n) {
            return !!n && (typeof n == "string" ? ci.test(n) ? i(n, this.context).index(this[0]) >= 0 : i.filter(n, this).length > 0 : this.filter(n).length > 0)
        },
        closest: function (n, t) {
            for (var r, f = 0, o = this.length, u = [], e = ci.test(n) || typeof n != "string" ? i(n, t || this.context) : 0; f < o; f++) for (r = this[f]; r && r.ownerDocument && r !== t && r.nodeType !== 11;) {
                if (e ? e.index(r) > -1 : i.find.matchesSelector(r, n)) {
                    u.push(r);
                    break
                }
                r = r.parentNode
            }
            return u = u.length > 1 ? i.unique(u) : u, this.pushStack(u, "closest", n)
        },
        index: function (n) {
            return n ? typeof n == "string" ? i.inArray(this[0], i(n)) : i.inArray(n.jquery ? n[0] : n, this) : this[0] && this[0].parentNode ? this.prevAll().length : -1
        },
        add: function (n, t) {
            var u = typeof n == "string" ? i(n, t) : i.makeArray(n && n.nodeType ? [n] : n),
                r = i.merge(this.get(), u);
            return this.pushStack(d(u[0]) || d(r[0]) ? r : i.unique(r))
        },
        addBack: function (n) {
            return this.add(n == null ? this.prevObject : this.prevObject.filter(n))
        }
    }), i.fn.andSelf = i.fn.addBack, i.each({
        parent: function (n) {
            var t = n.parentNode;
            return t && t.nodeType !== 11 ? t : null
        },
        parents: function (n) {
            return i.dir(n, "parentNode")
        },
        parentsUntil: function (n, t, r) {
            return i.dir(n, "parentNode", r)
        },
        next: function (n) {
            return kr(n, "nextSibling")
        },
        prev: function (n) {
            return kr(n, "previousSibling")
        },
        nextAll: function (n) {
            return i.dir(n, "nextSibling")
        },
        prevAll: function (n) {
            return i.dir(n, "previousSibling")
        },
        nextUntil: function (n, t, r) {
            return i.dir(n, "nextSibling", r)
        },
        prevUntil: function (n, t, r) {
            return i.dir(n, "previousSibling", r)
        },
        siblings: function (n) {
            return i.sibling((n.parentNode || {}).firstChild, n)
        },
        children: function (n) {
            return i.sibling(n.firstChild)
        },
        contents: function (n) {
            return i.nodeName(n, "iframe") ? n.contentDocument || n.contentWindow.document : i.merge([], n.childNodes)
        }
    }, function (n, t) {
        i.fn[n] = function (r, u) {
            var f = i.map(this, t, r);
            return eo.test(n) || (u = r), u && typeof u == "string" && (f = i.filter(u, f)), f = this.length > 1 && !po[n] ? i.unique(f) : f, this.length > 1 && oo.test(n) && (f = f.reverse()), this.pushStack(f, n, o.call(arguments).join(","))
        }
    }), i.extend({
        filter: function (n, t, r) {
            return r && (n = ":not(" + n + ")"), t.length === 1 ? i.find.matchesSelector(t[0], n) ? [t[0]] : [] : i.find.matches(n, t)
        },
        dir: function (n, r, u) {
            for (var e = [], f = n[r]; f && f.nodeType !== 9 && (u === t || f.nodeType !== 1 || !i(f).is(u));) f.nodeType === 1 && e.push(f), f = f[r];
            return e
        },
        sibling: function (n, t) {
            for (var i = []; n; n = n.nextSibling) n.nodeType === 1 && n !== t && i.push(n);
            return i
        }
    });
    var si = "abbr|article|aside|audio|bdi|canvas|data|datalist|details|figcaption|figure|footer|header|hgroup|mark|meter|nav|output|progress|section|summary|time|video",
        vo = / jQuery\d+="(?:null|\d+)"/g,
        bt = /^\s+/,
        or = /<(?!area|br|col|embed|hr|img|input|link|meta|param)(([\w:]+)[^>]*)\/>/gi,
        er = /<([\w:]+)/,
        lo = /<tbody/i,
        ao = /<|&#?\w+;/,
        fo = /<(?:script|style|link)/i,
        de = /<(?:script|object|embed|option|style)/i,
        kt = new RegExp("<(?:" + si + ")[\\s/>]", "i"),
        nu = /^(?:checkbox|radio)$/,
        sr = /checked\s*(?:[^=]|=\s*.checked.)/i,
        we = /\/(java|ecma)script/i,
        be = /^\s*<!(?:\[CDATA\[|\-\-)|[\]\-]{2}>\s*$/g,
        e = {
            option: [1, "<select multiple='multiple'>", "</select>"],
            legend: [1, "<fieldset>", "</fieldset>"],
            thead: [1, "<table>", "</table>"],
            tr: [2, "<table><tbody>", "</tbody></table>"],
            td: [3, "<table><tbody><tr>", "</tr></tbody></table>"],
            col: [2, "<table><tbody></tbody><colgroup>", "</colgroup></table>"],
            area: [1, "<map>", "</map>"],
            _default: [0, "", ""]
        }, nr = br(r),
        ni = nr.appendChild(r.createElement("div"));
    e.optgroup = e.option, e.tbody = e.tfoot = e.colgroup = e.caption = e.thead, e.th = e.td, i.support.htmlSerialize || (e._default = [1, "X<div>", "</div>"]), i.fn.extend({
        text: function (n) {
            return i.access(this, function (n) {
                return n === t ? i.text(this) : this.empty().append((this[0] && this[0].ownerDocument || r).createTextNode(n))
            }, null, n, arguments.length)
        },
        wrapAll: function (n) {
            if (i.isFunction(n)) return this.each(function (t) {
                i(this).wrapAll(n.call(this, t))
            });
            if (this[0]) {
                var t = i(n, this[0].ownerDocument).eq(0).clone(!0);
                this[0].parentNode && t.insertBefore(this[0]), t.map(function () {
                    for (var n = this; n.firstChild && n.firstChild.nodeType === 1;) n = n.firstChild;
                    return n
                }).append(this)
            }
            return this
        },
        wrapInner: function (n) {
            return i.isFunction(n) ? this.each(function (t) {
                i(this).wrapInner(n.call(this, t))
            }) : this.each(function () {
                var r = i(this),
                    t = r.contents();
                t.length ? t.wrapAll(n) : r.append(n)
            })
        },
        wrap: function (n) {
            var t = i.isFunction(n);
            return this.each(function (r) {
                i(this).wrapAll(t ? n.call(this, r) : n)
            })
        },
        unwrap: function () {
            return this.parent().each(function () {
                i.nodeName(this, "body") || i(this).replaceWith(this.childNodes)
            }).end()
        },
        append: function () {
            return this.domManip(arguments, !0, function (n) {
                (this.nodeType === 1 || this.nodeType === 11) && this.appendChild(n)
            })
        },
        prepend: function () {
            return this.domManip(arguments, !0, function (n) {
                (this.nodeType === 1 || this.nodeType === 11) && this.insertBefore(n, this.firstChild)
            })
        },
        before: function () {
            if (!d(this[0])) return this.domManip(arguments, !1, function (n) {
                this.parentNode.insertBefore(n, this)
            });
            if (arguments.length) {
                var n = i.clean(arguments);
                return this.pushStack(i.merge(n, this), "before", this.selector)
            }
        },
        after: function () {
            if (!d(this[0])) return this.domManip(arguments, !1, function (n) {
                this.parentNode.insertBefore(n, this.nextSibling)
            });
            if (arguments.length) {
                var n = i.clean(arguments);
                return this.pushStack(i.merge(this, n), "after", this.selector)
            }
        },
        remove: function (n, t) {
            for (var r, u = 0;
            (r = this[u]) != null; u++)(!n || i.filter(n, [r]).length) && (!t && r.nodeType === 1 && (i.cleanData(r.getElementsByTagName("*")), i.cleanData([r])), r.parentNode && r.parentNode.removeChild(r));
            return this
        },
        empty: function () {
            for (var n, t = 0;
            (n = this[t]) != null; t++) for (n.nodeType === 1 && i.cleanData(n.getElementsByTagName("*")); n.firstChild;) n.removeChild(n.firstChild);
            return this
        },
        clone: function (n, t) {
            return n = n == null ? !1 : n, t = t == null ? n : t, this.map(function () {
                return i.clone(this, n, t)
            })
        },
        html: function (n) {
            return i.access(this, function (n) {
                var r = this[0] || {}, u = 0,
                    f = this.length;
                if (n === t) return r.nodeType === 1 ? r.innerHTML.replace(vo, "") : t;
                if (typeof n == "string" && !fo.test(n) && (i.support.htmlSerialize || !kt.test(n)) && (i.support.leadingWhitespace || !bt.test(n)) && !e[(er.exec(n) || ["", ""])[1].toLowerCase()]) {
                    n = n.replace(or, "<$1></$2>");
                    try {
                        for (; u < f; u++) r = this[u] || {}, r.nodeType === 1 && (i.cleanData(r.getElementsByTagName("*")), r.innerHTML = n);
                        r = 0
                    } catch (o) {}
                }
                r && this.empty().append(n)
            }, null, n, arguments.length)
        },
        replaceWith: function (n) {
            return d(this[0]) ? this.length ? this.pushStack(i(i.isFunction(n) ? n() : n), "replaceWith", n) : this : i.isFunction(n) ? this.each(function (t) {
                var r = i(this),
                    u = r.html();
                r.replaceWith(n.call(this, t, u))
            }) : (typeof n != "string" && (n = i(n).detach()), this.each(function () {
                var t = this.nextSibling,
                    r = this.parentNode;
                i(this).remove(), t ? i(t).before(n) : i(r).append(n)
            }))
        },
        detach: function (n) {
            return this.remove(n, !0)
        },
        domManip: function (n, r, u) {
            n = [].concat.apply([], n);
            var l, o, f, a, e = 0,
                s = n[0],
                h = [],
                c = this.length;
            if (!i.support.checkClone && c > 1 && typeof s == "string" && sr.test(s)) return this.each(function () {
                i(this).domManip(n, r, u)
            });
            if (i.isFunction(s)) return this.each(function (f) {
                var e = i(this);
                n[0] = s.call(this, f, r ? e.html() : t), e.domManip(n, r, u)
            });
            if (this[0]) {
                if (l = i.buildFragment(n, this, h), f = l.fragment, o = f.firstChild, f.childNodes.length === 1 && (f = o), o) for (r = r && i.nodeName(o, "tr"), a = l.cacheable || c - 1; e < c; e++) u.call(r && i.nodeName(this[e], "table") ? uf(this[e], "tbody") : this[e], e === a ? f : i.clone(f, !0, !0));
                f = o = null, h.length && i.each(h, function (n, t) {
                    t.src ? i.ajax ? i.ajax({
                        url: t.src,
                        type: "GET",
                        dataType: "script",
                        async: !1,
                        global: !1,
                        throws: !0
                    }) : i.error("no ajax") : i.globalEval((t.text || t.textContent || t.innerHTML || "").replace(be, "")), t.parentNode && t.parentNode.removeChild(t)
                })
            }
            return this
        }
    }), i.buildFragment = function (n, u, f) {
        var o, s, h, e = n[0];
        return u = u || r, u = !u.nodeType && u[0] || u, u = u.ownerDocument || u, n.length === 1 && typeof e == "string" && e.length < 512 && u === r && e.charAt(0) === "<" && !de.test(e) && (i.support.checkClone || !sr.test(e)) && (i.support.html5Clone || !kt.test(e)) && (s = !0, o = i.fragments[e], h = o !== t), o || (o = u.createDocumentFragment(), i.clean(n, u, o, f), s && (i.fragments[e] = h && o)), {
            fragment: o,
            cacheable: s
        }
    }, i.fragments = {}, i.each({
        appendTo: "append",
        prependTo: "prepend",
        insertBefore: "before",
        insertAfter: "after",
        replaceAll: "replaceWith"
    }, function (n, t) {
        i.fn[n] = function (r) {
            var s, e = 0,
                o = [],
                f = i(r),
                h = f.length,
                u = this.length === 1 && this[0].parentNode;
            if ((u == null || u && u.nodeType === 11 && u.childNodes.length === 1) && h === 1) return f[t](this[0]), this;
            for (; e < h; e++) s = (e > 0 ? this.clone(!0) : this).get(), i(f[e])[t](s), o = o.concat(s);
            return this.pushStack(o, n, f.selector)
        }
    }), i.extend({
        clone: function (n, t, r) {
            var e, o, u, f;
            if (i.support.html5Clone || i.isXMLDoc(n) || !kt.test("<" + n.nodeName + ">") ? f = n.cloneNode(!0) : (ni.innerHTML = n.outerHTML, ni.removeChild(f = ni.firstChild)), (!i.support.noCloneEvent || !i.support.noCloneChecked) && (n.nodeType === 1 || n.nodeType === 11) && !i.isXMLDoc(n)) for (vi(n, f), e = ht(n), o = ht(f), u = 0; e[u]; ++u) o[u] && vi(e[u], o[u]);
            if (t && (wr(n, f), r)) for (e = ht(n), o = ht(f), u = 0; e[u]; ++u) wr(e[u], o[u]);
            return e = o = null, f
        },
        clean: function (n, t, u, f) {
            var h, l, o, w, v, d, s, k, a, g, b, p, y = t === r && nr,
                c = [];
            for (t && typeof t.createDocumentFragment != "undefined" || (t = r), h = 0;
            (o = n[h]) != null; h++) if (typeof o == "number" && (o += ""), o) {
                if (typeof o == "string") if (ao.test(o)) {
                    for (y = y || br(t), s = t.createElement("div"), y.appendChild(s), o = o.replace(or, "<$1></$2>"), w = (er.exec(o) || ["", ""])[1].toLowerCase(), v = e[w] || e._default, d = v[0], s.innerHTML = v[1] + o + v[2]; d--;) s = s.lastChild;
                    if (!i.support.tbody) for (k = lo.test(o), a = w === "table" && !k ? s.firstChild && s.firstChild.childNodes : v[1] === "<table>" && !k ? s.childNodes : [], l = a.length - 1; l >= 0; --l) i.nodeName(a[l], "tbody") && !a[l].childNodes.length && a[l].parentNode.removeChild(a[l]);
                    !i.support.leadingWhitespace && bt.test(o) && s.insertBefore(t.createTextNode(bt.exec(o)[0]), s.firstChild), o = s.childNodes, s.parentNode.removeChild(s)
                } else o = t.createTextNode(o);
                o.nodeType ? c.push(o) : i.merge(c, o)
            }
            if (s && (o = s = y = null), !i.support.appendChecked) for (h = 0;
            (o = c[h]) != null; h++) i.nodeName(o, "input") ? oi(o) : typeof o.getElementsByTagName != "undefined" && i.grep(o.getElementsByTagName("input"), oi);
            if (u) for (b = function (n) {
                if (!n.type || we.test(n.type)) return f ? f.push(n.parentNode ? n.parentNode.removeChild(n) : n) : u.appendChild(n)
            }, h = 0;
            (o = c[h]) != null; h++) i.nodeName(o, "script") && b(o) || (u.appendChild(o), typeof o.getElementsByTagName != "undefined" && (p = i.grep(i.merge([], o.getElementsByTagName("script")), b), c.splice.apply(c, [h + 1, 0].concat(p)), h += p.length));
            return c
        },
        cleanData: function (n, t) {
            for (var f, u, r, o, h = 0, e = i.expando, s = i.cache, l = i.support.deleteExpando, c = i.event.special;
            (r = n[h]) != null; h++) if ((t || i.acceptData(r)) && (u = r[e], f = u && s[u], f)) {
                if (f.events) for (o in f.events) c[o] ? i.event.remove(r, o) : i.removeEvent(r, o, f.handle);
                s[u] && (delete s[u], l ? delete r[e] : r.removeAttribute ? r.removeAttribute(e) : r[e] = null, i.deletedIds.push(u))
            }
        }
    }),
    function () {
        var t, n;
        i.uaMatch = function (n) {
            n = n.toLowerCase();
            var t = /(chrome)[ \/]([\w.]+)/.exec(n) || /(webkit)[ \/]([\w.]+)/.exec(n) || /(opera)(?:.*version|)[ \/]([\w.]+)/.exec(n) || /(msie) ([\w.]+)/.exec(n) || n.indexOf("compatible") < 0 && /(mozilla)(?:.*? rv:([\w.]+)|)/.exec(n) || [];
            return {
                browser: t[1] || "",
                version: t[2] || "0"
            }
        }, t = i.uaMatch(he.userAgent), n = {}, t.browser && (n[t.browser] = !0, n.version = t.version), n.chrome ? n.webkit = !0 : n.webkit && (n.safari = !0), i.browser = n, i.sub = function () {
            function n(t, i) {
                return new n.fn.init(t, i)
            }
            i.extend(!0, n, this), n.superclass = this, n.fn = n.prototype = this(), n.fn.constructor = n, n.sub = this.sub, n.fn.init = function (r, u) {
                return u && u instanceof i && !(u instanceof n) && (u = n(u)), i.fn.init.call(this, r, u, t)
            }, n.fn.init.prototype = n.fn;
            var t = n(r);
            return n
        }
    }();
    var u, v, p, vt = /alpha\([^)]*\)/i,
        uo = /opacity=([^)]*)/,
        io = /^(top|right|bottom|left)$/,
        no = /^(none|table(?!-c[ea]).+)/,
        ir = /^margin/,
        to = new RegExp("^(" + it + ")(.*)$", "i"),
        et = new RegExp("^(" + it + ")(?!px)[a-z%]+$", "i"),
        gu = new RegExp("^([-+])=(" + it + ")", "i"),
        ti = {
            BODY: "block"
        }, nf = {
            position: "absolute",
            visibility: "hidden",
            display: "block"
        }, hr = {
            letterSpacing: 0,
            fontWeight: 400
        }, h = ["Top", "Right", "Bottom", "Left"],
        lr = ["Webkit", "O", "Moz", "ms"],
        lf = i.fn.toggle;
    i.fn.extend({
        css: function (n, r) {
            return i.access(this, function (n, r, u) {
                return u !== t ? i.style(n, r, u) : i.css(n, r)
            }, n, r, arguments.length > 1)
        },
        show: function () {
            return cr(this, !0)
        },
        hide: function () {
            return cr(this)
        },
        toggle: function (n, t) {
            var r = typeof n == "boolean";
            return i.isFunction(n) && i.isFunction(t) ? lf.apply(this, arguments) : this.each(function () {
                (r ? n : lt(this)) ? i(this).show() : i(this).hide()
            })
        }
    }), i.extend({
        cssHooks: {
            opacity: {
                get: function (n, t) {
                    if (t) {
                        var i = u(n, "opacity");
                        return i === "" ? "1" : i
                    }
                }
            }
        },
        cssNumber: {
            fillOpacity: !0,
            fontWeight: !0,
            lineHeight: !0,
            opacity: !0,
            orphans: !0,
            widows: !0,
            zIndex: !0,
            zoom: !0
        },
        cssProps: {
            float: i.support.cssFloat ? "cssFloat" : "styleFloat"
        },
        style: function (n, r, u, f) {
            if (n && n.nodeType !== 3 && n.nodeType !== 8 && n.style) {
                var s, h, e, o = i.camelCase(r),
                    c = n.style;
                if (r = i.cssProps[o] || (i.cssProps[o] = hi(c, o)), e = i.cssHooks[r] || i.cssHooks[o], u === t) return e && "get" in e && (s = e.get(n, !1, f)) !== t ? s : c[r];
                if ((h = typeof u, h === "string" && (s = gu.exec(u)) && (u = (s[1] + 1) * s[2] + parseFloat(i.css(n, r)), h = "number"), u != null && (h !== "number" || !isNaN(u))) && (h === "number" && !i.cssNumber[o] && (u += "px"), !e || !("set" in e) || (u = e.set(n, u, f)) !== t)) try {
                    c[r] = u
                } catch (l) {}
            }
        },
        css: function (n, r, f, e) {
            var o, c, s, h = i.camelCase(r);
            return r = i.cssProps[h] || (i.cssProps[h] = hi(n.style, h)), s = i.cssHooks[r] || i.cssHooks[h], s && "get" in s && (o = s.get(n, !0, e)), o === t && (o = u(n, r)), o === "normal" && r in hr && (o = hr[r]), f || e !== t ? (c = parseFloat(o), f || i.isNumeric(c) ? c || 0 : o) : o
        },
        swap: function (n, t, i) {
            var f, r, u = {};
            for (r in t) u[r] = n.style[r], n.style[r] = t[r];
            f = i.call(n);
            for (r in t) n.style[r] = u[r];
            return f
        }
    }), n.getComputedStyle ? u = function (t, r) {
        var f, o, s, h, e = n.getComputedStyle(t, null),
            u = t.style;
        return e && (f = e.getPropertyValue(r) || e[r], f === "" && !i.contains(t.ownerDocument, t) && (f = i.style(t, r)), et.test(f) && ir.test(r) && (o = u.width, s = u.minWidth, h = u.maxWidth, u.minWidth = u.maxWidth = u.width = f, f = e.width, u.width = o, u.minWidth = s, u.maxWidth = h)), f
    } : r.documentElement.currentStyle && (u = function (n, t) {
        var f, u, i = n.currentStyle && n.currentStyle[t],
            r = n.style;
        return i == null && r && r[t] && (i = r[t]), et.test(i) && !io.test(t) && (f = r.left, u = n.runtimeStyle && n.runtimeStyle.left, u && (n.runtimeStyle.left = n.currentStyle.left), r.left = t === "fontSize" ? "1em" : i, i = r.pixelLeft + "px", r.left = f, u && (n.runtimeStyle.left = u)), i === "" ? "auto" : i
    }), i.each(["height", "width"], function (n, t) {
        i.cssHooks[t] = {
            get: function (n, r, f) {
                if (r) return n.offsetWidth === 0 && no.test(u(n, "display")) ? i.swap(n, nf, function () {
                    return ur(n, t, f)
                }) : ur(n, t, f)
            },
            set: function (n, r, u) {
                return rr(n, r, u ? wi(n, t, u, i.support.boxSizing && i.css(n, "boxSizing") === "border-box") : 0)
            }
        }
    }), i.support.opacity || (i.cssHooks.opacity = {
        get: function (n, t) {
            return uo.test((t && n.currentStyle ? n.currentStyle.filter : n.style.filter) || "") ? .01 * parseFloat(RegExp.$1) + "" : t ? "1" : ""
        },
        set: function (n, t) {
            var r = n.style,
                f = n.currentStyle,
                e = i.isNumeric(t) ? "alpha(opacity=" + t * 100 + ")" : "",
                u = f && f.filter || r.filter || "";
            (r.zoom = 1, t >= 1 && i.trim(u.replace(vt, "")) === "" && r.removeAttribute && (r.removeAttribute("filter"), f && !f.filter)) || (r.filter = vt.test(u) ? u.replace(vt, e) : u + " " + e)
        }
    }), i(function () {
        i.support.reliableMarginRight || (i.cssHooks.marginRight = {
            get: function (n, t) {
                return i.swap(n, {
                    display: "inline-block"
                }, function () {
                    if (t) return u(n, "marginRight")
                })
            }
        }), !i.support.pixelPosition && i.fn.position && i.each(["top", "left"], function (n, t) {
            i.cssHooks[t] = {
                get: function (n, r) {
                    if (r) {
                        var f = u(n, t);
                        return et.test(f) ? i(n).position()[t] + "px" : f
                    }
                }
            }
        })
    }), i.expr && i.expr.filters && (i.expr.filters.hidden = function (n) {
        return n.offsetWidth === 0 && n.offsetHeight === 0 || !i.support.reliableHiddenOffsets && (n.style && n.style.display || u(n, "display")) === "none"
    }, i.expr.filters.visible = function (n) {
        return !i.expr.filters.hidden(n)
    }), i.each({
        margin: "",
        padding: "",
        border: "Width"
    }, function (n, t) {
        i.cssHooks[n + t] = {
            expand: function (i) {
                for (var u = typeof i == "string" ? i.split(" ") : [i], f = {}, r = 0; r < 4; r++) f[n + h[r] + t] = u[r] || u[r - 2] || u[0];
                return f
            }
        }, ir.test(n) || (i.cssHooks[n + t].set = rr)
    });
    var cf = /%20/g,
        hf = /\[\]$/,
        fr = /\r?\n/g,
        yf = /^(?:color|date|datetime|datetime-local|email|hidden|month|number|password|range|search|tel|text|time|url|week)$/i,
        vf = /^(?:select|textarea)/i;
    i.fn.extend({
        serialize: function () {
            return i.param(this.serializeArray())
        },
        serializeArray: function () {
            return this.map(function () {
                return this.elements ? i.makeArray(this.elements) : this
            }).filter(function () {
                return this.name && !this.disabled && (this.checked || vf.test(this.nodeName) || yf.test(this.type))
            }).map(function (n, t) {
                var r = i(this).val();
                return r == null ? null : i.isArray(r) ? i.map(r, function (n) {
                    return {
                        name: t.name,
                        value: n.replace(fr, "\r\n")
                    }
                }) : {
                    name: t.name,
                    value: r.replace(fr, "\r\n")
                }
            }).get()
        }
    }), i.param = function (n, r) {
        var f, u = [],
            e = function (n, t) {
                t = i.isFunction(t) ? t() : t == null ? "" : t, u[u.length] = encodeURIComponent(n) + "=" + encodeURIComponent(t)
            };
        if (r === t && (r = i.ajaxSettings && i.ajaxSettings.traditional), i.isArray(n) || n.jquery && !i.isPlainObject(n)) i.each(n, function () {
            e(this.name, this.value)
        });
        else for (f in n) ri(f, n[f], r, e);
        return u.join("&").replace(cf, "+")
    };
    var a, l, of = /#.*$/,
        ef = /^(.*?):[ \t]*([^\r\n]*)\r?$/mg,
        ff = /^(?:about|app|app\-storage|.+\-extension|file|res|widget):$/,
        sf = /^(?:GET|HEAD)$/,
        af = /^\/\//,
        ei = /\?/,
        yu = /<script\b[^<]*(?:(?!<\/script>)<[^<]*)*<\/script>/gi,
        bu = /([?&])_=[^&]*/,
        li = /^([\w\+\.\-]+:)(?:\/\/([^\/?#:]*)(?::(\d+)|)|)/,
        ai = i.fn.load,
        gt = {}, ou = {}, vu = ["*/"] + ["*"];
    try {
        l = fe.href
    } catch (wo) {
        l = r.createElement("a"), l.href = "", l = l.href
    }
    a = li.exec(l.toLowerCase()) || [], i.fn.load = function (n, r, u) {
        if (typeof n != "string" && ai) return ai.apply(this, arguments);
        if (!this.length) return this;
        var e, o, h, s = this,
            f = n.indexOf(" ");
        return f >= 0 && (e = n.slice(f, n.length), n = n.slice(0, f)), i.isFunction(r) ? (u = r, r = t) : r && typeof r == "object" && (o = "POST"), i.ajax({
            url: n,
            type: o,
            dataType: "html",
            data: r,
            complete: function (n, t) {
                u && s.each(u, h || [n.responseText, t, n])
            }
        }).done(function (n) {
            h = arguments, s.html(e ? i("<div>").append(n.replace(yu, "")).find(e) : n)
        }), this
    }, i.each("ajaxStart ajaxStop ajaxComplete ajaxError ajaxSuccess ajaxSend".split(" "), function (n, t) {
        i.fn[t] = function (n) {
            return this.on(t, n)
        }
    }), i.each(["get", "post"], function (n, r) {
        i[r] = function (n, u, f, e) {
            return i.isFunction(u) && (e = e || f, f = u, u = t), i.ajax({
                type: r,
                url: n,
                data: u,
                success: f,
                dataType: e
            })
        }
    }), i.extend({
        getScript: function (n, r) {
            return i.get(n, t, r, "script")
        },
        getJSON: function (n, t, r) {
            return i.get(n, t, r, "json")
        },
        ajaxSetup: function (n, t) {
            return t ? gi(n, i.ajaxSettings) : (t = n, n = i.ajaxSettings), gi(n, t), n
        },
        ajaxSettings: {
            url: l,
            isLocal: ff.test(a[1]),
            global: !0,
            type: "GET",
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            processData: !0,
            async: !0,
            accepts: {
                xml: "application/xml, text/xml",
                html: "text/html",
                text: "text/plain",
                json: "application/json, text/javascript",
                "*": vu
            },
            contents: {
                xml: /xml/,
                html: /html/,
                json: /json/
            },
            responseFields: {
                xml: "responseXML",
                text: "responseText"
            },
            converters: {
                "* text": n.String,
                "text html": !0,
                "text json": i.parseJSON,
                "text xml": i.parseXML
            },
            flatOptions: {
                context: !0,
                url: !0
            }
        },
        ajaxPrefilter: di(gt),
        ajaxTransport: di(ou),
        ajax: function (n, r) {
            function p(n, r, h, c) {
                var a, d, w, tt, p, l = r;
                e !== 2 && (e = 2, nt && clearTimeout(nt), v = t, k = c || "", f.readyState = n > 0 ? 4 : 0, h && (tt = ro(u, f, h)), n >= 200 && n < 300 || n === 304 ? (u.ifModified && (p = f.getResponseHeader("Last-Modified"), p && (i.lastModified[o] = p), p = f.getResponseHeader("Etag"), p && (i.etag[o] = p)), n === 304 ? (l = "notmodified", a = !0) : (a = ge(u, tt), l = a.state, d = a.data, w = a.error, a = !w)) : (w = l, (!l || n) && (l = "error", n < 0 && (n = 0))), f.status = n, f.statusText = (r || l) + "", a ? it.resolveWith(s, [d, l, f]) : it.rejectWith(s, [f, l, w]), f.statusCode(b), b = t, y && g.trigger("ajax" + (a ? "Success" : "Error"), [f, u, a ? d : w]), ft.fireWith(s, [f, l]), y && (g.trigger("ajaxComplete", [f, u]), --i.active || i.event.trigger("ajaxStop")))
            }
            var d, tt;
            typeof n == "object" && (r = n, n = t), r = r || {};
            var o, k, w, v, nt, l, y, h, u = i.ajaxSetup({}, r),
                s = u.context || u,
                g = s !== u && (s.nodeType || s instanceof i) ? i(s) : i.event,
                it = i.Deferred(),
                ft = i.Callbacks("once memory"),
                b = u.statusCode || {}, et = {}, ot = {}, e = 0,
                rt = "canceled",
                f = {
                    readyState: 0,
                    setRequestHeader: function (n, t) {
                        if (!e) {
                            var i = n.toLowerCase();
                            n = ot[i] = ot[i] || n, et[n] = t
                        }
                        return this
                    },
                    getAllResponseHeaders: function () {
                        return e === 2 ? k : null
                    },
                    getResponseHeader: function (n) {
                        var i;
                        if (e === 2) {
                            if (!w) for (w = {}; i = ef.exec(k);) w[i[1].toLowerCase()] = i[2];
                            i = w[n.toLowerCase()]
                        }
                        return i === t ? null : i
                    },
                    overrideMimeType: function (n) {
                        return e || (u.mimeType = n), this
                    },
                    abort: function (n) {
                        return n = n || rt, v && v.abort(n), p(0, n), this
                    }
                };
            if (it.promise(f), f.success = f.done, f.error = f.fail, f.complete = ft.add, f.statusCode = function (n) {
                if (n) {
                    var t;
                    if (e < 2) for (t in n) b[t] = [b[t], n[t]];
            else t = n[f.status], f.always(t)
            }
                return this
            }, u.url = ((n || u.url) + "").replace(of, "").replace(af, a[1] + "//"), u.dataTypes = i.trim(u.dataType || "*").toLowerCase().split(c), u.crossDomain == null && (l = li.exec(u.url.toLowerCase()), u.crossDomain = !(!l || l[1] === a[1] && l[2] === a[2] && (l[3] || (l[1] === "http:" ? 80 : 443)) == (a[3] || (a[1] === "http:" ? 80 : 443)))), u.data && u.processData && typeof u.data != "string" && (u.data = i.param(u.data, u.traditional)), ut(gt, u, r, f), e === 2) return f;
            y = u.global, u.type = u.type.toUpperCase(), u.hasContent = !sf.test(u.type), y && i.active++ == 0 && i.event.trigger("ajaxStart"), u.hasContent || (u.data && (u.url += (ei.test(u.url) ? "&" : "?") + u.data, delete u.data), o = u.url, u.cache === !1 && (d = i.now(), tt = u.url.replace(bu, "$1_=" + d), u.url = tt + (tt === u.url ? (ei.test(u.url) ? "&" : "?") + "_=" + d : ""))), (u.data && u.hasContent && u.contentType !== !1 || r.contentType) && f.setRequestHeader("Content-Type", u.contentType), u.ifModified && (o = o || u.url, i.lastModified[o] && f.setRequestHeader("If-Modified-Since", i.lastModified[o]), i.etag[o] && f.setRequestHeader("If-None-Match", i.etag[o])), f.setRequestHeader("Accept", u.dataTypes[0] && u.accepts[u.dataTypes[0]] ? u.accepts[u.dataTypes[0]] + (u.dataTypes[0] !== "*" ? ", " + vu + "; q=0.01" : "") : u.accepts["*"]);
            for (h in u.headers) f.setRequestHeader(h, u.headers[h]);
            if (!u.beforeSend || u.beforeSend.call(s, f, u) !== !1 && e !== 2) {
                rt = "abort";
                for (h in {
                    success: 1,
                    error: 1,
                    complete: 1
                }) f[h](u[h]);
                if (v = ut(ou, u, r, f), v) {
                    f.readyState = 1, y && g.trigger("ajaxSend", [f, u]), u.async && u.timeout > 0 && (nt = setTimeout(function () {
                        f.abort("timeout")
                    }, u.timeout));
                    try {
                        e = 1, v.send(et, p)
                    } catch (st) {
                        if (!(e < 2)) throw st;
                        p(-1, st)
                    }
                } else p(-1, "No Transport");
                return f
            }
            return f.abort()
        },
        active: 0,
        lastModified: {},
        etag: {}
    });
    var pr = [],
        tf = /\?/,
        st = /(=)\?(?=&|$)|\?\?/,
        rf = i.now();
    i.ajaxSetup({
        jsonp: "callback",
        jsonpCallback: function () {
            var n = pr.pop() || i.expando + "_" + rf++;
            return this[n] = !0, n
        }
    }), i.ajaxPrefilter("json jsonp", function (r, u, f) {
        var e, s, o, l = r.data,
            a = r.url,
            h = r.jsonp !== !1,
            c = h && st.test(a),
            v = h && !c && typeof l == "string" && !(r.contentType || "").indexOf("application/x-www-form-urlencoded") && st.test(l);
        if (r.dataTypes[0] === "jsonp" || c || v) return e = r.jsonpCallback = i.isFunction(r.jsonpCallback) ? r.jsonpCallback() : r.jsonpCallback, s = n[e], c ? r.url = a.replace(st, "$1" + e) : v ? r.data = l.replace(st, "$1" + e) : h && (r.url += (tf.test(a) ? "&" : "?") + r.jsonp + "=" + e), r.converters["script json"] = function () {
            return o || i.error(e + " was not called"), o[0]
        }, r.dataTypes[0] = "json", n[e] = function () {
            o = arguments
        }, f.always(function () {
            n[e] = s, r[e] && (r.jsonpCallback = u.jsonpCallback, pr.push(e)), o && i.isFunction(s) && s(o[0]), o = s = t
        }), "script"
    }), i.ajaxSetup({
        accepts: {
            script: "text/javascript, application/javascript, application/ecmascript, application/x-ecmascript"
        },
        contents: {
            script: /javascript|ecmascript/
        },
        converters: {
            "text script": function (n) {
                return i.globalEval(n), n
            }
        }
    }), i.ajaxPrefilter("script", function (n) {
        n.cache === t && (n.cache = !1), n.crossDomain && (n.type = "GET", n.global = !1)
    }), i.ajaxTransport("script", function (n) {
        if (n.crossDomain) {
            var i, u = r.head || r.getElementsByTagName("head")[0] || r.documentElement;
            return {
                send: function (f, e) {
                    i = r.createElement("script"), i.async = "async", n.scriptCharset && (i.charset = n.scriptCharset), i.src = n.url, i.onload = i.onreadystatechange = function (n, r) {
                        (r || !i.readyState || /loaded|complete/.test(i.readyState)) && (i.onload = i.onreadystatechange = null, u && i.parentNode && u.removeChild(i), i = t, r || e(200, "success"))
                    }, u.insertBefore(i, u.firstChild)
                },
                abort: function () {
                    i && i.onload(0, 1)
                }
            }
        }
    }), ot = n.ActiveXObject ? function () {
        for (var n in b) b[n](0, 1)
    } : !1, vr = 0, i.ajaxSettings.xhr = n.ActiveXObject ? function () {
        return !this.isLocal && ki() || pe()
    } : ki,
    function (n) {
        i.extend(i.support, {
            ajax: !! n,
            cors: !! n && "withCredentials" in n
        })
    }(i.ajaxSettings.xhr()), i.support.ajax && i.ajaxTransport(function (r) {
        if (!r.crossDomain || i.support.cors) {
            var u;
            return {
                send: function (f, e) {
                    var h, s, o = r.xhr();
                    if (r.username ? o.open(r.type, r.url, r.async, r.username, r.password) : o.open(r.type, r.url, r.async), r.xhrFields) for (s in r.xhrFields) o[s] = r.xhrFields[s];
                    r.mimeType && o.overrideMimeType && o.overrideMimeType(r.mimeType), !r.crossDomain && !f["X-Requested-With"] && (f["X-Requested-With"] = "XMLHttpRequest");
                    try {
                        for (s in f) o.setRequestHeader(s, f[s])
                    } catch (c) {}
                    o.send(r.hasContent && r.data || null), u = function (n, f) {
                        var c, a, v, s, l;
                        try {
                            if (u && (f || o.readyState === 4)) if (u = t, h && (o.onreadystatechange = i.noop, ot && delete b[h]), f) o.readyState !== 4 && o.abort();
                            else {
                                c = o.status, v = o.getAllResponseHeaders(), s = {}, l = o.responseXML, l && l.documentElement && (s.xml = l);
                                try {
                                    s.text = o.responseText
                                } catch (p) {}
                                try {
                                    a = o.statusText
                                } catch (p) {
                                    a = ""
                                }!c && r.isLocal && !r.crossDomain ? c = s.text ? 200 : 404 : c === 1223 && (c = 204)
                            }
                        } catch (y) {
                            f || e(-1, y)
                        }
                        s && e(c, a, s, v)
                    }, r.async ? o.readyState === 4 ? setTimeout(u, 0) : (h = ++vr, ot && (b || (b = {}, i(n).unload(ot)), b[h] = u), o.onreadystatechange = u) : u()
                },
                abort: function () {
                    u && u(0, 1)
                }
            }
        }
    });
    var w, ft, wu = /^(?:toggle|show|hide)$/,
        pu = new RegExp("^(?:([-+])=|)(" + it + ")([a-z%]*)$", "i"),
        du = /queueHooks$/,
        tt = [ee],
        k = {
            "*": [function (n, t) {
                var s, o, r = this.createTween(n, t),
                    e = pu.exec(t),
                    h = r.cur(),
                    f = +h || 0,
                    u = 1,
                    c = 20;
                if (e) {
                    if (s = +e[2], o = e[3] || (i.cssNumber[n] ? "" : "px"), o !== "px" && f) {
                        f = i.css(r.elem, n, !0) || s || 1;
                        do u = u || ".5", f /= u, i.style(r.elem, n, f + o);
                        while (u !== (u = r.cur() / h) && u !== 1 && --c)
                    }
                    r.unit = o, r.start = f, r.end = e[1] ? f + (e[1] + 1) * s : s
                }
                return r
            }]
        };
    i.Animation = i.extend(au, {
        tweener: function (n, t) {
            i.isFunction(n) ? (t = n, n = ["*"]) : n = n.split(" ");
            for (var r, u = 0, f = n.length; u < f; u++) r = n[u], k[r] = k[r] || [], k[r].unshift(t)
        },
        prefilter: function (n, t) {
            t ? tt.unshift(n) : tt.push(n)
        }
    }), i.Tween = f, f.prototype = {
        constructor: f,
        init: function (n, t, r, u, f, e) {
            this.elem = n, this.prop = r, this.easing = f || "swing", this.options = t, this.start = this.now = this.cur(), this.end = u, this.unit = e || (i.cssNumber[r] ? "" : "px")
        },
        cur: function () {
            var n = f.propHooks[this.prop];
            return n && n.get ? n.get(this) : f.propHooks._default.get(this)
        },
        run: function (n) {
            var r, t = f.propHooks[this.prop];
            return this.pos = this.options.duration ? r = i.easing[this.easing](n, this.options.duration * n, 0, 1, this.options.duration) : r = n, this.now = (this.end - this.start) * r + this.start, this.options.step && this.options.step.call(this.elem, this.now, this), t && t.set ? t.set(this) : f.propHooks._default.set(this), this
        }
    }, f.prototype.init.prototype = f.prototype, f.propHooks = {
        _default: {
            get: function (n) {
                var t;
                return n.elem[n.prop] == null || !! n.elem.style && n.elem.style[n.prop] != null ? (t = i.css(n.elem, n.prop, !1, ""), !t || t === "auto" ? 0 : t) : n.elem[n.prop]
            },
            set: function (n) {
                i.fx.step[n.prop] ? i.fx.step[n.prop](n) : n.elem.style && (n.elem.style[i.cssProps[n.prop]] != null || i.cssHooks[n.prop]) ? i.style(n.elem, n.prop, n.now + n.unit) : n.elem[n.prop] = n.now
            }
        }
    }, f.propHooks.scrollTop = f.propHooks.scrollLeft = {
        set: function (n) {
            n.elem.nodeType && n.elem.parentNode && (n.elem[n.prop] = n.now)
        }
    }, i.each(["toggle", "show", "hide"], function (n, t) {
        var r = i.fn[t];
        i.fn[t] = function (u, f, e) {
            return u == null || typeof u == "boolean" || !n && i.isFunction(u) && i.isFunction(f) ? r.apply(this, arguments) : this.animate(nt(t, !0), u, f, e)
        }
    }), i.fn.extend({
        fadeTo: function (n, t, i, r) {
            return this.filter(lt).css("opacity", 0).show().end().animate({
                opacity: t
            }, n, i, r)
        },
        animate: function (n, t, r, u) {
            var o = i.isEmptyObject(n),
                f = i.speed(t, r, u),
                e = function () {
                    var t = au(this, i.extend({}, n), f);
                    o && t.stop(!0)
                };
            return o || f.queue === !1 ? this.each(e) : this.queue(f.queue, e)
        },
        stop: function (n, r, u) {
            var f = function (n) {
                var t = n.stop;
                delete n.stop, t(u)
            };
            return typeof n != "string" && (u = r, r = n, n = t), r && n !== !1 && this.queue(n || "fx", []), this.each(function () {
                var o = !0,
                    t = n != null && n + "queueHooks",
                    e = i.timers,
                    r = i._data(this);
                if (t) r[t] && r[t].stop && f(r[t]);
                else for (t in r) r[t] && r[t].stop && du.test(t) && f(r[t]);
                for (t = e.length; t--;) e[t].elem === this && (n == null || e[t].queue === n) && (e[t].anim.stop(u), o = !1, e.splice(t, 1));
                (o || !u) && i.dequeue(this, n)
            })
        }
    }), i.each({
        slideDown: nt("show"),
        slideUp: nt("hide"),
        slideToggle: nt("toggle"),
        fadeIn: {
            opacity: "show"
        },
        fadeOut: {
            opacity: "hide"
        },
        fadeToggle: {
            opacity: "toggle"
        }
    }, function (n, t) {
        i.fn[n] = function (n, i, r) {
            return this.animate(t, n, i, r)
        }
    }), i.speed = function (n, t, r) {
        var u = n && typeof n == "object" ? i.extend({}, n) : {
            complete: r || !r && t || i.isFunction(n) && n,
            duration: n,
            easing: r && t || t && !i.isFunction(t) && t
        };
        return u.duration = i.fx.off ? 0 : typeof u.duration == "number" ? u.duration : u.duration in i.fx.speeds ? i.fx.speeds[u.duration] : i.fx.speeds._default, (u.queue == null || u.queue === !0) && (u.queue = "fx"), u.old = u.complete, u.complete = function () {
            i.isFunction(u.old) && u.old.call(this), u.queue && i.dequeue(this, u.queue)
        }, u
    }, i.easing = {
        linear: function (n) {
            return n
        },
        swing: function (n) {
            return .5 - Math.cos(n * Math.PI) / 2
        }
    }, i.timers = [], i.fx = f.prototype.init, i.fx.tick = function () {
        var u, r = i.timers,
            n = 0;
        for (w = i.now(); n < r.length; n++) u = r[n], !u() && r[n] === u && r.splice(n--, 1);
        r.length || i.fx.stop(), w = t
    }, i.fx.timer = function (n) {
        n() && i.timers.push(n) && !ft && (ft = setInterval(i.fx.tick, i.fx.interval))
    }, i.fx.interval = 13, i.fx.stop = function () {
        clearInterval(ft), ft = null
    }, i.fx.speeds = {
        slow: 600,
        fast: 200,
        _default: 400
    }, i.fx.step = {}, i.expr && i.expr.filters && (i.expr.filters.animated = function (n) {
        return i.grep(i.timers, function (t) {
            return n === t.elem
        }).length
    }), at = /^(?:body|html)$/i, i.fn.offset = function (n) {
        if (arguments.length) return n === t ? this : this.each(function (t) {
            i.offset.setOffset(this, n, t)
        });
        var u, s, o, c, h, l, a, e = {
            top: 0,
            left: 0
        }, r = this[0],
            f = r && r.ownerDocument;
        if (f) return (s = f.body) === r ? i.offset.bodyOffset(r) : (u = f.documentElement, i.contains(u, r) ? (typeof r.getBoundingClientRect != "undefined" && (e = r.getBoundingClientRect()), o = tu(f), c = u.clientTop || s.clientTop || 0, h = u.clientLeft || s.clientLeft || 0, l = o.pageYOffset || u.scrollTop, a = o.pageXOffset || u.scrollLeft, {
            top: e.top + l - c,
            left: e.left + a - h
        }) : e)
    }, i.offset = {
        bodyOffset: function (n) {
            var r = n.offsetTop,
                t = n.offsetLeft;
            return i.support.doesNotIncludeMarginInBodyOffset && (r += parseFloat(i.css(n, "marginTop")) || 0, t += parseFloat(i.css(n, "marginLeft")) || 0), {
                top: r,
                left: t
            }
        },
        setOffset: function (n, t, r) {
            var s = i.css(n, "position");
            s === "static" && (n.style.position = "relative");
            var h = i(n),
                c = h.offset(),
                l = i.css(n, "top"),
                a = i.css(n, "left"),
                v = (s === "absolute" || s === "fixed") && i.inArray("auto", [l, a]) > -1,
                u = {}, e = {}, f, o;
            v ? (e = h.position(), f = e.top, o = e.left) : (f = parseFloat(l) || 0, o = parseFloat(a) || 0), i.isFunction(t) && (t = t.call(n, r, c)), t.top != null && (u.top = t.top - c.top + f), t.left != null && (u.left = t.left - c.left + o), "using" in t ? t.using.call(n, u) : h.css(u)
        }
    }, i.fn.extend({
        position: function () {
            if (this[0]) {
                var u = this[0],
                    r = this.offsetParent(),
                    n = this.offset(),
                    t = at.test(r[0].nodeName) ? {
                        top: 0,
                        left: 0
                    } : r.offset();
                return n.top -= parseFloat(i.css(u, "marginTop")) || 0, n.left -= parseFloat(i.css(u, "marginLeft")) || 0, t.top += parseFloat(i.css(r[0], "borderTopWidth")) || 0, t.left += parseFloat(i.css(r[0], "borderLeftWidth")) || 0, {
                    top: n.top - t.top,
                    left: n.left - t.left
                }
            }
        },
        offsetParent: function () {
            return this.map(function () {
                for (var n = this.offsetParent || r.body; n && !at.test(n.nodeName) && i.css(n, "position") === "static";) n = n.offsetParent;
                return n || r.body
            })
        }
    }), i.each({
        scrollLeft: "pageXOffset",
        scrollTop: "pageYOffset"
    }, function (n, r) {
        var u = /Y/.test(r);
        i.fn[n] = function (f) {
            return i.access(this, function (n, f, e) {
                var o = tu(n);
                if (e === t) return o ? r in o ? o[r] : o.document.documentElement[f] : n[f];
                o ? o.scrollTo(u ? i(o).scrollLeft() : e, u ? e : i(o).scrollTop()) : n[f] = e
            }, n, f, arguments.length, null)
        }
    }), i.each({
        Height: "height",
        Width: "width"
    }, function (n, r) {
        i.each({
            padding: "inner" + n,
            content: r,
            "": "outer" + n
        }, function (u, f) {
            i.fn[f] = function (f, e) {
                var s = arguments.length && (u || typeof f != "boolean"),
                    o = u || (f === !0 || e === !0 ? "margin" : "border");
                return i.access(this, function (r, u, f) {
                    var e;
                    return i.isWindow(r) ? r.document.documentElement["client" + n] : r.nodeType === 9 ? (e = r.documentElement, Math.max(r.body["scroll" + n], e["scroll" + n], r.body["offset" + n], e["offset" + n], e["client" + n])) : f === t ? i.css(r, u, f, o) : i.style(r, u, f, o)
                }, r, s ? f : t, s, null)
            }
        })
    }), n.jQuery = n.$ = i, typeof define == "function" && define.amd && define.amd.jQuery && define("jquery", [], function () {
        return i
    })
})(window);
var Cufon = function () {
    function ft(n) {
        var t = this.face = n.face,
            i = {
                " ": 1,
                " ": 1,
                "　": 1
            };
        this.glyphs = n.glyphs, this.w = n.w, this.baseSize = parseInt(t["units-per-em"], 10), this.family = t["font-family"].toLowerCase(), this.weight = t["font-weight"], this.style = t["font-style"] || "normal", this.viewBox = function () {
            var i = t.bbox.split(/\s+/),
                n = {
                    minX: parseInt(i[0], 10),
                    minY: parseInt(i[1], 10),
                    maxX: parseInt(i[2], 10),
                    maxY: parseInt(i[3], 10)
                };
            return n.width = n.maxX - n.minX, n.height = n.maxY - n.minY, n.toString = function () {
                return [this.minX, this.minY, this.width, this.height].join(" ")
            }, n
        }(), this.ascent = -parseInt(t.ascent, 10), this.descent = -parseInt(t.descent, 10), this.height = -this.ascent + this.descent, this.spacing = function (n, t, r) {
            for (var l = this.glyphs, e, s, c, f = [], o = 0, a = -1, h = -1, u; u = n[++a];)(e = l[u] || this.missingGlyph, e) && (s && (o -= c = s[u] || 0, f[h] -= c), o += f[++h] = ~~ (e.w || this.w) + t + (i[u] ? r : 0), s = e.k);
            return f.total = o, f
        }
    }
    function ut() {
        var n = {}, t = {
            oblique: "italic",
            italic: "oblique"
        };
        this.add = function (t) {
            (n[t.style] || (n[t.style] = {}))[t.weight] = t
        }, this.get = function (i, r) {
            var e = n[i] || n[t[i]] || n.normal || n.italic || n.oblique,
                h, c, s, o, u;
            if (!e) return null;
            if (r = {
                normal: 400,
                bold: 700
            }[r] || parseInt(r, 10), e[r]) return e[r];
            h = {
                1: 1,
                99: 0
            }[r % 100], c = [], h === undefined && (h = r > 400), r == 500 && (r = 400);
            for (u in e) f(e, u) && (u = parseInt(u, 10), (!s || u < s) && (s = u), (!o || u > o) && (o = u), c.push(u));
            return r < s && (r = s), r > o && (r = o), c.sort(function (n, t) {
                return (h ? n >= r && t >= r ? n < t : n > t : n <= r && t <= r ? n > t : n < t) ? -1 : 1
            }), e[c[0]]
        }
    }
    function d() {
        function f(n, t) {
            return n.contains ? n.contains(t) : n.compareDocumentPosition(t) & 16
        }
        function u(n) {
            var t = n.relatedTarget;
            t && !f(this, t) && i(this, n.type == "mouseover")
        }
        function t(n) {
            i(this, n.type == "mouseenter")
        }
        function i(t, i) {
            setTimeout(function () {
                var r = a.get(t).options;
                n.replace(t, i ? l(r, r.hover) : r, !0)
            }, 10)
        }
        this.attach = function (n) {
            n.onmouseenter === undefined ? (r(n, "mouseover", u), r(n, "mouseout", u)) : (r(n, "mouseenter", t), r(n, "mouseleave", t))
        }
    }
    function nt() {
        function r(n) {
            for (var u = [], f, r = 0; f = n[r]; ++r) u[r] = t[i[f]];
            return u
        }
        var t = [],
            i = {};
        this.add = function (n, r) {
            i[n] = t.push(r) - 1
        }, this.repeat = function () {
            for (var f = arguments.length ? r(arguments) : t, i, u = 0; i = f[u++];) n.replace(i[0], i[1], !0)
        }
    }
    function g() {
        function t(n) {
            return n.cufid || (n.cufid = ++i)
        }
        var n = {}, i = 0;
        this.get = function (i) {
            var r = t(i);
            return n[r] || (n[r] = {})
        }
    }
    function c(n) {
        var i = {}, r = {};
        this.extend = function (n) {
            for (var t in n) f(n, t) && (i[t] = n[t]);
            return this
        }, this.get = function (t) {
            return i[t] != undefined ? i[t] : n[t]
        }, this.getSize = function (n, i) {
            return r[n] || (r[n] = new t.Size(this.get(n), i))
        }, this.isUsable = function () {
            return !!n
        }
    }
    function r(n, t, i) {
        n.addEventListener ? n.addEventListener(t, i, !1) : n.attachEvent && n.attachEvent("on" + t, function () {
            return i.call(n, window.event)
        })
    }
    function k(n, t) {
        var i = a.get(n);
        return i.options ? n : (t.hover && t.hoverables[n.nodeName.toLowerCase()] && b.attach(n), i.options = t, n)
    }
    function i(n) {
        var t = {};
        return function (i) {
            return f(t, i) || (t[i] = n.apply(null, arguments)), t[i]
        }
    }
    function tt(n, i) {
        for (var e = t.quotedList(i.get("fontFamily").toLowerCase()), r, f = 0; r = e[f]; ++f) if (u[r]) return u[r].get(i.get("fontStyle"), i.get("fontWeight"));
        return null
    }
    function e(n) {
        return document.getElementsByTagName(n)
    }
    function f(n, t) {
        return n.hasOwnProperty(t)
    }
    function l() {
        for (var u = {}, t, i, n = 0, r = arguments.length; t = arguments[n], n < r; ++n) for (i in t) f(t, i) && (u[i] = t[i]);
        return u
    }
    function it(n, i, r, u, f, e) {
        var l = document.createDocumentFragment(),
            a, o, c;
        if (i === "") return l;
        var v = u.separate,
            s = i.split(w[v]),
            y = v == "words";
        for (y && p && (/^\s/.test(i) && s.unshift(""), /\s$/.test(i) && s.push("")), o = 0, c = s.length; o < c; ++o) a = h[u.engine](n, y ? t.textAlign(s[o], r, o, c) : s[o], r, u, f, e, o < c - 1), a && l.appendChild(a);
        return l
    }
    function rt(n, i) {
        var l = n.nodeName.toLowerCase();
        if (!i.ignore[l]) {
            var a = !i.textless[l],
                f = t.getStyle(k(n, i)).extend(i),
                s = tt(n, f),
                r, o, e, u, v, c;
            if (s) for (r = n.firstChild; r; r = e)(o = r.nodeType, e = r.nextSibling, a && o == 3 && (u ? (u.appendData(r.data), n.removeChild(r)) : u = r, e)) || (u && (n.replaceChild(it(s, t.whiteSpace(u.data, f, u, c), f, i, r, n), u), u = null), o == 1 && (r.firstChild && (r.nodeName.toLowerCase() == "cufon" ? h[i.engine](s, null, f, i, r, n) : arguments.callee(r, i)), c = r))
        }
    }
    var n = function () {
        return n.replace.apply(null, arguments)
    }, o = n.DOM = {
        ready: function () {
            var t = !1,
                u = {
                    loaded: 1,
                    complete: 1
                }, i = [],
                n = function () {
                    if (!t) {
                        t = !0;
                        for (var n; n = i.shift(); n());
                    }
                };
            return document.addEventListener && (document.addEventListener("DOMContentLoaded", n, !1), window.addEventListener("pageshow", n, !1)), !window.opera && document.readyState && function () {
                u[document.readyState] ? n() : setTimeout(arguments.callee, 10)
            }(), document.readyState && document.createStyleSheet && function () {
                try {
                    document.body.doScroll("left"), n()
                } catch (t) {
                    setTimeout(arguments.callee, 1)
                }
            }(), r(window, "load", n),
            function (r) {
                arguments.length ? t ? r() : i.push(r) : n()
            }
        }(),
        root: function () {
            return document.documentElement || document.body
        }
    }, t = n.CSS = {
        Size: function (n, t) {
            this.value = parseFloat(n), this.unit = String(n).match(/[a-z%]*$/)[0] || "px", this.convert = function (n) {
                return n / t * this.value
            }, this.convertFrom = function (n) {
                return n / this.value * t
            }, this.toString = function () {
                return this.value + this.unit
            }
        },
        addClass: function (n, t) {
            var i = n.className;
            return n.className = i + (i && " ") + t, n
        },
        color: i(function (n) {
            var t = {};
            return t.color = n.replace(/^rgba\((.*?),\s*([\d.]+)\)/, function (n, i, r) {
                return t.opacity = parseFloat(r), "rgb(" + i + ")"
            }), t
        }),
        fontStretch: i(function (n) {
            return typeof n == "number" ? n : /%$/.test(n) ? parseFloat(n) / 100 : {
                "ultra-condensed": .5,
                "extra-condensed": .625,
                condensed: .75,
                "semi-condensed": .875,
                "semi-expanded": 1.125,
                expanded: 1.25,
                "extra-expanded": 1.5,
                "ultra-expanded": 2
            }[n] || 1
        }),
        getStyle: function (n) {
            var t = document.defaultView;
            return t && t.getComputedStyle ? new c(t.getComputedStyle(n, null)) : n.currentStyle ? new c(n.currentStyle) : new c(n.style)
        },
        gradient: i(function (n) {
            for (var f = {
                id: n,
                type: n.match(/^-([a-z]+)-gradient\(/)[1],
                stops: []
            }, u = n.substr(n.indexOf("(")).match(/([\d.]+=)?(#[a-f0-9]+|[a-z]+\(.*?\)|[a-z]+)/ig), r, t = 0, i = u.length; t < i; ++t) r = u[t].split("=", 2).reverse(), f.stops.push([r[1] || t / (i - 1), r[0]]);
            return f
        }),
        quotedList: i(function (n) {
            for (var i = [], r = /\s*((["'])([\s\S]*?[^\\])\2|[^,]+)\s*/g, t; t = r.exec(n);) i.push(t[3] || t[1]);
            return i
        }),
        recognizesMedia: i(function (n) {
            var t = document.createElement("style"),
                r, i, u;
            t.type = "text/css", t.media = n;
            try {
                t.appendChild(document.createTextNode("/**/"))
            } catch (f) {}
            return i = e("head")[0], i.insertBefore(t, i.firstChild), r = t.sheet || t.styleSheet, u = r && !r.disabled, i.removeChild(t), u
        }),
        removeClass: function (n, t) {
            var i = RegExp("(?:^|\\s+)" + t + "(?=\\s|$)", "g");
            return n.className = n.className.replace(i, ""), n
        },
        supports: function (n, t) {
            var i = document.createElement("span").style;
            return i[n] === undefined ? !1 : (i[n] = t, i[n] === t)
        },
        textAlign: function (n, t, i, r) {
            return t.get("textAlign") == "right" ? i > 0 && (n = " " + n) : i < r - 1 && (n += " "), n
        },
        textShadow: i(function (n) {
            if (n == "none") return null;
            for (var r = [], i = {}, t, u = 0, f = /(#[a-f0-9]+|[a-z]+\(.*?\)|[a-z]+)|(-?[\d.]+[a-z%]*)|,/ig; t = f.exec(n);) t[0] == "," ? (r.push(i), i = {}, u = 0) : t[1] ? i.color = t[1] : i[["offX", "offY", "blur"][u++]] = t[2];
            return r.push(i), r
        }),
        textTransform: function () {
            var n = {
                uppercase: function (n) {
                    return n.toUpperCase()
                },
                lowercase: function (n) {
                    return n.toLowerCase()
                },
                capitalize: function (n) {
                    return n.replace(/\b./g, function (n) {
                        return n.toUpperCase()
                    })
                }
            };
            return function (t, i) {
                var r = n[i.get("textTransform")];
                return r ? r(t) : t
            }
        }(),
        whiteSpace: function () {
            var i = {
                inline: 1,
                "inline-block": 1,
                "run-in": 1
            }, n = /^\s+/,
                t = /\s+$/;
            return function (r, u, f, e) {
                return (e && e.nodeName.toLowerCase() == "br" && (r = r.replace(n, "")), i[u.get("display")]) ? r : (f.previousSibling || (r = r.replace(n, "")), f.nextSibling || (r = r.replace(t, "")), r)
            }
        }()
    };
    t.ready = function () {
        function u(n) {
            return n.disabled || r(n.sheet, n.media || "screen")
        }
        function r(n, i) {
            var e, f, u, o;
            if (!t.recognizesMedia(i || "all")) return !0;
            if (!n || n.disabled) return !1;
            try {
                if (e = n.cssRules, e) n: for (u = 0, o = e.length; f = e[u], u < o; ++u) switch (f.type) {
                    case 2:
                        break;
                    case 3:
                        if (!r(f.styleSheet, f.media.mediaText)) return !1;
                        break;
                    default:
                        break n
                }
            } catch (s) {}
            return !0
        }
        function s() {
            if (document.createStyleSheet) return !0;
            for (var t, n = 0; t = c[n]; ++n) if (t.rel.toLowerCase() == "stylesheet" && !u(t)) return !1;
            for (n = 0; t = l[n]; ++n) if (!u(t)) return !1;
            return !0
        }
        var i = !t.recognizesMedia("all"),
            n = !1,
            f = [],
            h = function () {
                i = !0;
                for (var n; n = f.shift(); n());
            }, c = e("link"),
            l = e("style");
        return o.ready(function () {
            n || (n = t.getStyle(document.body).isUsable()), i || n && s() ? h() : setTimeout(arguments.callee, 10)
        }),
        function (n) {
            i ? n() : f.push(n)
        }
    }();
    var p = " ".split(/\s+/).length == 0,
        a = new g,
        b = new d,
        s = new nt,
        y = !1,
        h = {}, u = {}, v = {
            autoDetect: !1,
            engine: null,
            forceHitArea: !1,
            hover: !1,
            hoverables: {
                a: !0
            },
            ignore: {
                applet: 1,
                canvas: 1,
                col: 1,
                colgroup: 1,
                head: 1,
                iframe: 1,
                map: 1,
                optgroup: 1,
                option: 1,
                script: 1,
                select: 1,
                style: 1,
                textarea: 1,
                title: 1,
                pre: 1
            },
            printable: !0,
            selector: window.Sizzle || window.jQuery && function (n) {
                return jQuery(n)
            } || window.dojo && dojo.query || window.Ext && Ext.query || window.YAHOO && YAHOO.util && YAHOO.util.Selector && YAHOO.util.Selector.query || window.$$ && function (n) {
                return $$(n)
            } || window.$ && function (n) {
                return $(n)
            } || document.querySelectorAll && function (n) {
                return document.querySelectorAll(n)
            } || e,
            separate: "words",
            textless: {
                dl: 1,
                html: 1,
                ol: 1,
                table: 1,
                tbody: 1,
                thead: 1,
                tfoot: 1,
                tr: 1,
                ul: 1
            },
            textShadow: "none"
        }, w = {
            words: /\s/.test(" ") ? /[^\S\u00a0]+/ : /\s+/,
            characters: "",
            none: /^/
        };
    return n.now = function () {
        return o.ready(), n
    }, n.refresh = function () {
        return s.repeat.apply(s, arguments), n
    }, n.registerEngine = function (t, i) {
        return i ? (h[t] = i, n.set("engine", t)) : n
    }, n.registerFont = function (t) {
        if (!t) return n;
        var r = new ft(t),
            i = r.family;
        return u[i] || (u[i] = new ut), u[i].add(r), n.set("fontFamily", '"' + i + '"')
    }, n.replace = function (i, r, u) {
        return (r = l(v, r), !r.engine) ? n : (y || (t.addClass(o.root(), "cufon-active cufon-loading"), t.ready(function () {
            t.addClass(t.removeClass(o.root(), "cufon-loading"), "cufon-ready")
        }), y = !0), r.hover && (r.forceHitArea = !0), r.autoDetect && delete r.fontFamily, typeof r.textShadow == "string" && (r.textShadow = t.textShadow(r.textShadow)), typeof r.color == "string" && /^-/.test(r.color) ? r.textGradient = t.gradient(r.color) : delete r.textGradient, u || s.add(i, arguments), (i.nodeType || typeof i == "string") && (i = [i]), t.ready(function () {
            for (var t, u = 0, f = i.length; u < f; ++u) t = i[u], typeof t == "string" ? n.replace(r.selector(t), r, !0) : rt(t, r)
        }), n)
    }, n.set = function (t, i) {
        return v[t] = i, n
    }, n
}();
Cufon.registerEngine("vml", function () {
    function o(n, t) {
        return i(n, /(?:em|ex|%)$|^[a-z-]+$/i.test(t) ? "1em" : t)
    }
    function i(n, t) {
        var u, r, i;
        return t === "0" ? 0 : /px$/i.test(t) ? parseFloat(t) : (u = n.style.left, r = n.runtimeStyle.left, n.runtimeStyle.left = n.currentStyle.left, n.style.left = t.replace("%", "em"), i = n.style.pixelLeft, n.style.left = u, n.runtimeStyle.left = r, i)
    }
    function f(n, t, r, u) {
        var e = "computed" + u,
            f = t[e];
        return isNaN(f) && (f = t.get(u), t[e] = f = f == "normal" ? 0 : ~~r.convertFrom(i(n, f))), f
    }
    function e(n) {
        var e = n.id,
            r, f;
        if (!t[e]) {
            var u = n.stops,
                i = document.createElement("cvml:fill"),
                o = [];
            for (i.type = "gradient", i.angle = 180, i.focus = "0", i.method = "sigma", i.color = u[0][1], r = 1, f = u.length - 1; r < f; ++r) o.push(u[r][0] * 100 + "% " + u[r][1]);
            i.colors = o.join(","), i.color2 = u[f][1], t[e] = i
        }
        return t[e]
    }
    var r = document.namespaces,
        n, u, t;
    if (r) return (r.add("cvml", "urn:schemas-microsoft-com:vml"), r = null, n = document.createElement("cvml:shape"), n.style.behavior = "url(#default#VML)", !n.coordsize) ? void 0 : (n = null, u = (document.documentMode || 0) < 8, document.write(('<style type="text/css">cufoncanvas{text-indent:0;}@media screen{cvml\\:shape,cvml\\:rect,cvml\\:fill,cvml\\:shadow{behavior:url(#default#VML);display:block;antialias:true;position:absolute;}cufoncanvas{position:absolute;text-align:left;}cufon{display:inline-block;position:relative;vertical-align:' + (u ? "middle" : "text-bottom") + ";}cufon cufontext{position:absolute;left:-10000in;font-size:1px;}a cufon{cursor:pointer}}@media print{cufon cufoncanvas{display:none;}}</style>").replace(/;/g, "!important;")), t = {}, function (n, t, r, s, h, c, l) {
        var yt = t === null,
            g, p, y, b, ct, ft, a, st, v, at, lt, d, k;
        yt && (t = h.alt), g = n.viewBox, p = r.computedFontSize || (r.computedFontSize = new Cufon.CSS.Size(o(c, r.get("fontSize")) + "px", n.baseSize)), yt ? (y = h, b = h.firstChild) : (y = document.createElement("cufon"), y.className = "cufon cufon-vml", y.alt = t, b = document.createElement("cufoncanvas"), y.appendChild(b), s.printable && (ct = document.createElement("cufontext"), ct.appendChild(document.createTextNode(t)), y.appendChild(ct)), l || y.appendChild(document.createElement("cvml:shape")));
        var tt = y.style,
            ht = b.style,
            ti = p.convert(g.height),
            ut = Math.ceil(ti),
            hi = ut / ti,
            ii = hi * Cufon.CSS.fontStretch(r.get("fontStretch")),
            vt = g.minX,
            ri = g.minY;
        ht.height = ut, ht.top = Math.round(p.convert(ri - n.ascent)), ht.left = Math.round(p.convert(vt)), tt.height = p.convert(n.height) + "px";
        var vi = r.get("color"),
            pt = Cufon.CSS.textTransform(t, r).split(""),
            nt = n.spacing(pt, f(c, r, p, "letterSpacing"), f(c, r, p, "wordSpacing"));
        if (!nt.length) return null;
        for (var dt = nt.total, fi = -vt + dt + (g.width - nt[nt.length - 1]), ci = p.convert(fi * ii), gt = Math.round(ci), ni = fi + "," + g.height, oi, li = "r" + ni + "ns", ui = s.textGradient && e(s.textGradient), si = n.glyphs, ei = 0, ot = s.textShadow, bt = -1, kt = 0, ai; ai = pt[++bt];) if (ft = si[pt[bt]] || n.missingGlyph, ft) {
            if (yt) for (a = b.childNodes[kt]; a.firstChild;) a.removeChild(a.firstChild);
            else a = document.createElement("cvml:shape"), b.appendChild(a);
            if (a.stroked = "f", a.coordsize = ni, a.coordorigin = oi = vt - ei + "," + ri, a.path = (ft.d ? "m" + ft.d + "xe" : "") + "m" + oi + li, a.fillcolor = vi, ui && a.appendChild(ui.cloneNode(!1)), st = a.style, st.width = gt, st.height = ut, ot) {
                var et = ot[0],
                    it = ot[1],
                    wt = Cufon.CSS.color(et.color),
                    rt, w = document.createElement("cvml:shadow");
                w.on = "t", w.color = wt.color, w.offset = et.offX + "," + et.offY, it && (rt = Cufon.CSS.color(it.color), w.type = "double", w.color2 = rt.color, w.offset2 = it.offX + "," + it.offY), w.opacity = wt.opacity || rt && rt.opacity || 1, a.appendChild(w)
            }
            ei += nt[kt++]
        }
        return v = a.nextSibling, s.forceHitArea ? (v || (v = document.createElement("cvml:rect"), v.stroked = "f", v.className = "cufon-vml-cover", at = document.createElement("cvml:fill"), at.opacity = 0, v.appendChild(at), b.appendChild(v)), lt = v.style, lt.width = gt, lt.height = ut) : v && b.removeChild(v), tt.width = Math.max(Math.ceil(p.convert(dt * ii)), 0), u && (d = r.computedYAdjust, d === undefined && (k = r.get("lineHeight"), k == "normal" ? k = "1em" : isNaN(k) || (k += "em"), r.computedYAdjust = d = .5 * (i(c, k) - parseFloat(tt.height))), d && (tt.marginTop = Math.ceil(d) + "px", tt.marginBottom = d + "px")), y
    })
}()), Cufon.registerEngine("canvas", function () {
    function u(n, t) {
        var e = 0,
            f = 0,
            u = [],
            s = /([mrvxe])([^a-z]*)/g,
            o, r, i;
        n: for (r = 0; o = s.exec(n); ++r) {
            i = o[2].split(",");
            switch (o[1]) {
                case "v":
                    u[r] = {
                        m: "bezierCurveTo",
                        a: [e + ~~i[0], f + ~~i[1], e + ~~i[2], f + ~~i[3], e += ~~i[4], f += ~~i[5]]
                    };
                    break;
                case "r":
                    u[r] = {
                        m: "lineTo",
                        a: [e += ~~i[0], f += ~~i[1]]
                    };
                    break;
                case "m":
                    u[r] = {
                        m: "moveTo",
                        a: [e = ~~i[0], f = ~~i[1]]
                    };
                    break;
                case "x":
                    u[r] = {
                        m: "closePath"
                    };
                    break;
                case "e":
                    break n
            }
            t[u[r].m].apply(t, u[r].a)
        }
        return u
    }
    function r(n, t) {
        for (var r, i = 0, u = n.length; i < u; ++i) r = n[i], t[r.m].apply(t, r.a)
    }
    var n = document.createElement("canvas");
    if (n && n.getContext && n.getContext.apply) {
        n = null;
        var i = Cufon.CSS.supports("display", "inline-block"),
            f = !i && (document.compatMode == "BackCompat" || /frameset|transitional/i.test(document.doctype.publicId)),
            t = document.createElement("style");
        return t.type = "text/css", t.appendChild(document.createTextNode(("cufon{text-indent:0;}@media screen,projection{cufon{display:inline;display:inline-block;position:relative;vertical-align:middle;" + (f ? "" : "font-size:1px;line-height:1px;") + "}cufon cufontext{display:-moz-inline-box;display:inline-block;width:0;height:0;overflow:hidden;text-indent:-10000in;}" + (i ? "cufon canvas{position:relative;}" : "cufon canvas{position:absolute;}") + "}@media print{cufon{padding:0;}cufon canvas{display:none;}}").replace(/;/g, "!important;"))), document.getElementsByTagName("head")[0].appendChild(t),
        function (n, t, f, e, o) {
            function vt() {
                var o = n.glyphs,
                    i = -1,
                    e = -1,
                    f, t;
                for (h.scale(dt, 1); f = ut[++i];)(t = o[ut[i]] || n.missingGlyph, t) && (t.d && (h.beginPath(), t.code ? r(t.code, h) : t.code = u("m" + t.d, h), h.fill()), h.translate(p[++e], 0));
                h.restore()
            }
            var yt = t === null,
                ut, p, kt, a, y, at, et, h, ot, rt, st, ht, ft, l, pt;
            yt && (t = o.getAttribute("alt"));
            var v = n.viewBox,
                c = f.getSize("fontSize", n.baseSize),
                k = 0,
                nt = 0,
                ct = 0,
                b = 0,
                w = e.textShadow,
                bt = [];
            if (w) for (l = w.length; l--;) {
                var rt = w[l],
                    g = c.convertFrom(parseFloat(rt.offX)),
                    d = c.convertFrom(parseFloat(rt.offY));
                bt[l] = [g, d], d < k && (k = d), g > nt && (nt = g), d > ct && (ct = d), g < b && (b = g)
            }
            if (ut = Cufon.CSS.textTransform(t, f).split(""), p = n.spacing(ut, ~~c.convertFrom(parseFloat(f.get("letterSpacing")) || 0), ~~c.convertFrom(parseFloat(f.get("wordSpacing")) || 0)), !p.length) return null;
            kt = p.total, nt += v.width - p[p.length - 1], b += v.minX, yt ? (a = o, y = o.firstChild) : (a = document.createElement("cufon"), a.className = "cufon cufon-canvas", a.setAttribute("alt", t), y = document.createElement("canvas"), a.appendChild(y), e.printable && (at = document.createElement("cufontext"), at.appendChild(document.createTextNode(t)), a.appendChild(at)));
            var tt = a.style,
                it = y.style,
                lt = c.convert(v.height),
                ii = Math.ceil(lt),
                wt = ii / lt,
                dt = wt * Cufon.CSS.fontStretch(f.get("fontStretch")),
                ti = kt * dt,
                gt = Math.ceil(c.convert(ti + nt - b)),
                ni = Math.ceil(c.convert(v.height - k + ct));
            if (y.width = gt, y.height = ni, it.width = gt + "px", it.height = ni + "px", k += v.minY, it.top = Math.round(c.convert(k - n.ascent)) + "px", it.left = Math.round(c.convert(b)) + "px", et = Math.max(Math.ceil(c.convert(ti)), 0) + "px", i ? (tt.width = et, tt.height = c.convert(n.height) + "px") : (tt.paddingLeft = et, tt.paddingBottom = c.convert(n.height) - 1 + "px"), h = y.getContext("2d"), ot = lt / v.height, h.scale(ot, ot * wt), h.translate(-b, -k), h.save(), w) for (l = w.length; l--;) rt = w[l], h.save(), h.fillStyle = rt.color, h.translate.apply(h, bt[l]), vt();
            if (st = e.textGradient, st) {
                for (ht = st.stops, ft = h.createLinearGradient(0, v.minY, 0, v.maxY), l = 0, pt = ht.length; l < pt; ++l) ft.addColorStop.apply(ft, ht[l]);
                h.fillStyle = ft
            } else h.fillStyle = f.get("color");
            return vt(), a
        }
    }
}()), Cufon.registerFont({
    w: 172,
    face: {
        "font-family": "Helvetica Neue LT Pro",
        "font-weight": 700,
        "font-stretch": "normal",
        "units-per-em": "360",
        "panose-1": "2 11 7 6 3 5 2 3 2 4",
        ascent: "257",
        descent: "-103",
        "x-height": "5",
        bbox: "-9 -324 278 77.5943",
        "underline-thickness": "18",
        "underline-position": "-18",
        stemh: "38",
        stemv: "50",
        "unicode-range": "U+0020-U+015F"
    },
    glyphs: {
        " ": {
            w: 86
        },
        "!": {
            d: "28,-48r50,0r0,48r-50,0r0,-48xm71,-72r-36,0r-7,-102r0,-83r50,0v1,65,-2,126,-7,185",
            w: 106
        },
        '"': {
            d: "26,-141r0,-116r45,0r0,116r-45,0xm96,-141r0,-116r45,0r0,116r-45,0",
            w: 166
        },
        "#": {
            d: "72,-147r-6,44r35,0r6,-44r-35,0xm0,-63r0,-40r31,0r6,-44r-26,0r0,-40r31,0r8,-63r34,0r-7,63r35,0r8,-63r34,0r-7,63r26,0r0,40r-31,0r-6,44r26,0r0,40r-31,0r-8,63r-34,0r7,-63r-35,0r-7,63r-35,0r8,-63r-27,0"
        },
        $: {
            d: "74,36r0,-31v-62,-7,-72,-37,-71,-81r50,0v0,20,2,38,21,43r0,-73v-39,-10,-68,-35,-68,-75v0,-44,25,-69,68,-74r0,-25r23,0r0,25v55,6,67,36,66,73r-49,0v0,-18,-4,-31,-17,-35r0,68v35,16,73,23,73,77v0,52,-25,74,-73,77r0,31r-23,0xm74,-159r0,-58v-24,6,-24,49,0,58xm97,-97r0,64v31,-7,25,-56,0,-64"
        },
        "%": {
            d: "50,11r147,-272r32,0r-147,272r-32,0xm48,-188v0,29,2,43,16,43v14,0,16,-14,16,-43v0,-26,-2,-40,-16,-40v-14,0,-16,14,-16,40xm10,-186v0,-46,9,-69,54,-69v45,0,54,23,54,69v0,46,-9,69,-54,69v-45,0,-54,-23,-54,-69xm162,-64v0,-46,9,-68,54,-68v45,0,54,22,54,68v0,46,-9,69,-54,69v-45,0,-54,-23,-54,-69xm201,-65v0,29,1,43,15,43v14,0,16,-14,16,-43v0,-26,-2,-40,-16,-40v-14,0,-15,14,-15,40",
            w: 280
        },
        "&": {
            d: "115,-137r38,52v5,-11,9,-27,9,-38r45,0v0,28,-11,53,-26,75r34,48r-56,0r-14,-20v-13,15,-36,25,-62,25v-65,0,-79,-45,-79,-74v0,-30,20,-54,52,-74v-39,-40,-37,-116,39,-114v85,2,80,87,20,120xm123,-51r-43,-62v-20,15,-26,28,-26,44v0,31,52,48,69,18xm94,-225v-35,2,-19,42,-3,57v23,-11,42,-54,3,-57",
            w: 213
        },
        "(": {
            d: "68,-257r39,0v-56,102,-56,221,0,323r-39,0v-68,-115,-68,-208,0,-323",
            w: 106
        },
        ")": {
            d: "0,-257r38,0v69,115,69,208,0,323r-38,0v56,-102,56,-221,0,-323",
            w: 106
        },
        "*": {
            d: "55,-257r30,0r0,46r44,-14r9,28r-44,14r27,38r-24,17r-27,-38r-27,38r-24,-17r27,-38r-43,-14r9,-28r43,14r0,-46",
            w: 140
        },
        "+": {
            d: "86,0r0,-69r-69,0r0,-44r69,0r0,-69r44,0r0,69r69,0r0,44r-69,0r0,69r-44,0",
            w: 216
        },
        ",": {
            d: "19,-56r48,0v0,56,8,112,-48,115r0,-25v15,-4,20,-17,19,-34r-19,0r0,-56",
            w: 86,
            k: {
                " ": 13
            }
        },
        "-": {
            d: "16,-125r101,0r0,42r-101,0r0,-42",
            w: 133
        },
        ".": {
            d: "19,-56r48,0r0,56r-48,0r0,-56",
            w: 86,
            k: {
                " ": 13
            }
        },
        "/": {
            d: "-1,5r81,-267r41,0r-82,267r-40,0",
            w: 119
        },
        "0": {
            d: "6,-119v0,-71,4,-136,80,-136v62,0,80,34,80,124v0,71,-4,136,-80,136v-62,0,-80,-34,-80,-124xm58,-135r0,27v0,64,6,79,28,79v23,0,28,-19,28,-85r0,-28v0,-64,-6,-79,-28,-79v-23,0,-28,20,-28,86"
        },
        "1": {
            d: "117,0r-51,0r0,-180r-51,0r0,-34v36,1,59,-12,64,-41r38,0r0,255"
        },
        "2": {
            d: "163,-40r0,40r-157,0v-4,-53,39,-98,78,-131v23,-20,26,-32,26,-58v0,-21,-8,-32,-25,-32v-26,0,-29,23,-29,50r-50,0v-3,-55,21,-83,80,-84v94,-2,94,106,36,151v-19,15,-50,44,-55,64r96,0"
        },
        "3": {
            d: "7,-75r50,0v0,30,5,46,28,46v25,0,29,-19,29,-41v0,-30,-15,-48,-51,-43r0,-34v32,5,46,-13,46,-41v0,-24,-7,-33,-24,-33v-22,0,-27,17,-27,41r-47,0v0,-50,26,-75,75,-75v46,0,73,21,73,66v1,32,-17,50,-39,57v33,5,46,28,46,60v0,35,-14,77,-85,77v-50,0,-75,-30,-74,-80"
        },
        "4": {
            d: "5,-53r0,-42r85,-160r53,0r0,162r25,0r0,40r-25,0r0,53r-47,0r0,-53r-91,0xm96,-93r-1,-97r-50,97r51,0"
        },
        "5": {
            d: "153,-250r0,40r-94,0r-5,58v10,-13,25,-19,47,-19v49,0,62,42,62,82v0,56,-16,94,-81,94v-68,0,-77,-41,-76,-78r50,0v0,17,3,42,25,42v21,0,31,-17,31,-50v0,-42,-7,-54,-30,-54v-15,0,-23,10,-25,24r-46,0r9,-139r133,0"
        },
        "6": {
            d: "59,-79v0,29,5,50,29,50v25,0,28,-21,28,-50v0,-35,-8,-48,-28,-48v-21,0,-29,13,-29,48xm162,-190r-50,0v0,-20,-6,-31,-23,-31v-30,0,-35,41,-31,77v9,-13,24,-22,47,-22v45,0,63,34,63,80v0,59,-26,91,-83,91v-71,0,-80,-57,-80,-119v0,-72,2,-141,86,-141v37,0,70,16,71,65"
        },
        "7": {
            d: "8,-250r157,0r0,40v-42,59,-66,138,-75,210r-54,0v8,-62,29,-137,79,-205r-107,0r0,-45"
        },
        "8": {
            d: "57,-73v0,24,6,44,29,44v23,0,30,-20,30,-44v0,-24,-7,-44,-30,-44v-23,0,-29,20,-29,44xm61,-186v0,24,10,36,25,36v15,0,26,-12,26,-36v0,-23,-9,-35,-26,-35v-17,0,-25,12,-25,35xm5,-72v-1,-33,16,-56,43,-64v-69,-24,-32,-132,38,-119v71,-12,107,93,39,119v27,9,43,31,43,64v0,29,-11,77,-82,77v-71,0,-81,-48,-81,-77"
        },
        "9": {
            d: "57,-171v0,35,8,49,28,49v21,0,28,-14,28,-49v0,-29,-4,-50,-28,-50v-25,0,-28,21,-28,50xm11,-59r50,0v0,19,6,30,23,30v30,0,35,-41,31,-77v-9,13,-25,22,-48,22v-45,0,-62,-34,-62,-80v0,-59,25,-91,82,-91v71,0,81,57,81,119v0,72,-2,141,-86,141v-37,0,-70,-15,-71,-64"
        },
        ":": {
            d: "19,-131r0,-57r48,0r0,57r-48,0xm19,0r0,-56r48,0r0,56r-48,0",
            w: 86
        },
        ";": {
            d: "19,-188r48,0r0,57r-48,0r0,-57xm19,-56r48,0v0,56,8,112,-48,115r0,-25v15,-4,20,-17,19,-34r-19,0r0,-56",
            w: 86
        },
        ";": {
            d: "19,-188r48,0r0,57r-48,0r0,-57xm19,-56r48,0v0,56,8,112,-48,115r0,-25v15,-4,20,-17,19,-34r-19,0r0,-56",
            w: 86
        },
        "<": {
            d: "17,-72r0,-38r182,-75r0,45r-122,49r122,49r0,45",
            w: 216
        },
        "=": {
            d: "17,-30r0,-44r182,0r0,44r-182,0xm17,-108r0,-45r182,0r0,45r-182,0",
            w: 216
        },
        ">": {
            d: "17,3r0,-45r122,-49r-122,-49r0,-45r182,75r0,38",
            w: 216
        },
        "?": {
            d: "58,-48r50,0r0,48r-50,0r0,-48xm60,-177r-47,0v-2,-50,20,-82,71,-83v77,-2,97,77,54,126v-15,17,-37,32,-33,63r-44,0v-4,-41,14,-65,36,-86v20,-19,21,-68,-11,-69v-21,0,-27,23,-26,49",
            w: 173
        },
        "@": {
            d: "188,-176r5,-19r31,0r-23,105v0,5,2,7,7,7v14,0,38,-24,38,-62v0,-48,-46,-83,-96,-83v-57,0,-99,48,-99,100v0,55,47,99,102,99v33,0,58,-11,78,-23r32,0v-26,36,-65,57,-119,57v-74,0,-134,-60,-134,-134v0,-74,60,-133,134,-133v72,0,134,43,134,109v0,76,-68,103,-91,103v-15,1,-20,-8,-24,-19v-31,34,-96,20,-96,-42v0,-63,79,-124,121,-65xm148,-162v-38,-2,-58,69,-15,74v40,3,61,-70,15,-74",
            w: 288
        },
        A: {
            d: "75,-97r50,0r-25,-115xm0,0r68,-257r64,0r68,257r-54,0r-12,-54r-68,0r-12,54r-54,0",
            w: 200,
            k: {
                y: 6,
                w: 6,
                v: 6,
                Y: 20,
                W: 4,
                V: 4,
                T: 20
            }
        },
        B: {
            d: "70,-116r0,78v35,1,62,-1,62,-39v0,-39,-27,-40,-62,-39xm70,-219r0,67v33,2,56,-3,56,-33v0,-31,-23,-36,-56,-34xm18,0r0,-257v75,2,160,-18,160,65v0,29,-15,48,-39,57v29,4,47,25,47,60v0,40,-21,75,-81,75r-87,0",
            w: 200
        },
        C: {
            d: "180,-170r-52,0v0,-37,-8,-54,-31,-54v-26,0,-33,26,-33,99v0,78,12,92,34,92v18,0,32,-9,32,-65r52,0v0,56,-14,103,-82,103v-78,0,-87,-56,-87,-134v0,-78,9,-133,87,-133v74,0,80,55,80,92",
            w: 193,
            k: {
                A: 6
            }
        },
        D: {
            d: "72,-219r0,181v58,4,66,-10,67,-93v0,-69,-9,-94,-67,-88xm21,0r0,-257r84,0v72,0,85,49,85,125v0,91,-19,132,-88,132r-81,0",
            w: 206,
            k: {
                Y: 20,
                A: 12,
                ".": 9,
                ",": 9
            }
        },
        E: {
            d: "18,-257r144,0r0,42r-92,0r0,61r86,0r0,42r-86,0r0,70r95,0r0,42r-147,0r0,-257",
            w: 173
        },
        F: {
            d: "18,0r0,-257r144,0r0,42r-92,0r0,61r86,0r0,42r-86,0r0,112r-52,0",
            w: 166,
            k: {
                e: 7,
                a: 7,
                A: 13,
                ".": 40,
                ",": 40
            }
        },
        G: {
            d: "182,-181r-50,0v0,-19,-8,-43,-29,-43v-25,0,-36,26,-36,90v0,62,6,101,36,101v21,0,38,-32,32,-67r-34,0r0,-39r84,0r0,139r-38,0v-1,-7,2,-18,-1,-24v-13,20,-30,29,-54,29v-64,0,-77,-47,-77,-136v0,-86,21,-131,87,-131v57,0,80,29,80,81",
            w: 200
        },
        H: {
            d: "18,0r0,-257r52,0r0,98r60,0r0,-98r52,0r0,257r-52,0r0,-114r-60,0r0,114r-52,0",
            w: 200
        },
        I: {
            d: "72,0r-51,0r0,-257r51,0r0,257",
            w: 92
        },
        J: {
            d: "99,-257r52,0r0,185v0,55,-26,77,-78,77v-59,0,-69,-38,-68,-86r48,0v-1,27,1,48,23,48v19,0,23,-13,23,-40r0,-184",
            w: 166
        },
        K: {
            d: "18,-257r52,0r1,103r65,-103r56,0r-70,110r78,147r-58,0r-53,-105r-19,29r0,76r-52,0r0,-257",
            w: 193
        },
        L: {
            d: "162,0r-144,0r0,-257r52,0r0,215r92,0r0,42",
            w: 166,
            k: {
                y: 13,
                Y: 33,
                W: 27,
                V: 27,
                T: 27
            }
        },
        M: {
            d: "19,0r0,-257r77,0r38,181r37,-181r76,0r0,257r-47,0r-1,-205r-46,205r-40,0r-46,-205r0,205r-48,0",
            w: 266
        },
        N: {
            d: "18,-257r60,0r63,176r0,-176r48,0r0,257r-59,0r-64,-180r0,180r-48,0r0,-257",
            w: 206
        },
        O: {
            d: "13,-129v0,-78,9,-133,87,-133v78,0,88,55,88,133v0,78,-10,134,-88,134v-78,0,-87,-56,-87,-134xm64,-129v0,69,5,96,36,96v31,0,36,-27,36,-96v0,-69,-5,-95,-36,-95v-31,0,-36,26,-36,95",
            w: 200,
            k: {
                Y: 9,
                T: 9,
                A: 9,
                ".": 9,
                ",": 9
            }
        },
        P: {
            d: "70,-219r0,78v34,2,57,-2,57,-38v0,-35,-21,-43,-57,-40xm18,0r0,-257r91,0v53,0,70,38,70,77v0,64,-44,82,-109,77r0,103r-52,0",
            w: 186,
            k: {
                "ö": 9,
                o: 9,
                e: 9,
                a: 9,
                A: 19,
                ".": 54,
                ",": 54
            }
        },
        Q: {
            d: "64,-129v0,69,5,96,36,96v31,0,36,-27,36,-96v0,-69,-5,-95,-36,-95v-31,0,-36,26,-36,95xm134,0v-11,4,-20,5,-34,5v-78,0,-87,-56,-87,-134v0,-78,9,-133,87,-133v78,0,88,55,88,133v0,44,-3,81,-20,105r27,28r-30,28",
            w: 200
        },
        R: {
            d: "18,0r0,-257v76,1,164,-16,164,66v0,35,-14,58,-43,64v35,5,42,25,42,79v0,29,3,39,11,48r-56,0v-19,-30,13,-108,-38,-109r-28,0r0,109r-52,0xm70,-219r0,74v35,2,60,-1,60,-38v0,-34,-26,-38,-60,-36",
            w: 200,
            k: {
                "Ü": -4,
                Y: 6,
                W: -4,
                U: -4,
                T: 6
            }
        },
        S: {
            d: "8,-80r52,0v-1,28,4,47,34,47v16,0,31,-10,31,-32v0,-23,-11,-32,-46,-44v-46,-15,-67,-35,-67,-77v0,-50,30,-76,79,-76v48,0,83,22,80,76r-50,0v0,-24,-8,-38,-28,-38v-42,0,-40,52,-3,65v40,14,89,37,89,85v0,54,-33,79,-89,79v-63,0,-84,-30,-82,-85",
            w: 186
        },
        T: {
            d: "4,-257r164,0r0,42r-56,0r0,215r-52,0r0,-215r-56,0r0,-42",
            k: {
                "ü": 27,
                "ö": 27,
                y: 20,
                w: 27,
                u: 27,
                r: 27,
                o: 27,
                i: 6,
                e: 27,
                a: 27,
                A: 20,
                ";": 27,
                ":": 27,
                ".": 33,
                "-": 20,
                ",": 33
            }
        },
        U: {
            d: "15,-257r52,0r0,179v0,26,6,45,30,45v24,0,30,-19,30,-45r0,-179r52,0r0,179v0,65,-40,83,-82,83v-42,0,-82,-14,-82,-83r0,-179",
            w: 193
        },
        V: {
            d: "1,-257r56,0r37,190r38,-190r54,0r-59,257r-67,0",
            w: 186,
            k: {
                "ü": 11,
                "ö": 9,
                u: 11,
                o: 9,
                i: 9,
                e: 9,
                a: 6,
                A: 13,
                ";": 6,
                ":": 6,
                ".": 26,
                "-": 6,
                ",": 33
            }
        },
        W: {
            d: "50,0r-47,-257r50,0r29,190r30,-190r50,0r31,190r28,-190r50,0r-47,257r-58,0r-30,-186r-28,186r-58,0",
            w: 273,
            k: {
                "ö": 6,
                o: 6,
                e: 6,
                a: 6,
                A: 6,
                ".": 27,
                "-": 6,
                ",": 27
            }
        },
        X: {
            d: "66,-130r-59,-127r57,0r34,83r32,-83r57,0r-60,127r64,130r-58,0r-37,-87r-37,87r-57,0",
            w: 193
        },
        Y: {
            d: "1,-257r58,0r35,99r36,-99r56,0r-66,156r0,101r-52,0r0,-101",
            w: 187,
            k: {
                "Ş": 6,
                "ü": 22,
                "ö": 29,
                "Ö": 7,
                u: 22,
                o: 29,
                i: 9,
                e: 29,
                a: 29,
                S: 6,
                O: 7,
                A: 20,
                ";": 13,
                ":": 13,
                ".": 40,
                "-": 27,
                ",": 40
            }
        },
        Z: {
            d: "9,0r0,-38r94,-177r-88,0r0,-42r148,0r0,39r-96,176r98,0r0,42r-156,0",
            w: 173
        },
        "[": {
            d: "29,66r0,-323r82,0r0,36r-38,0r0,251r38,0r0,36r-82,0",
            w: 113
        },
        "\\": {
            d: "80,5r-81,-267r40,0r82,267r-41,0",
            w: 119
        },
        "]": {
            d: "2,66r0,-36r38,0r0,-251r-38,0r0,-36r83,0r0,323r-83,0",
            w: 113
        },
        "^": {
            d: "65,-111r-45,0r68,-139r40,0r68,139r-45,0r-43,-89",
            w: 216
        },
        _: {
            d: "180,45r-180,0r0,-18r180,0r0,18",
            w: 180
        },
        a: {
            d: "61,-134r-45,0v-2,-48,29,-65,68,-65v119,0,46,114,77,199r-48,0v-4,-5,-3,-15,-7,-20v-12,20,-25,25,-49,25v-35,0,-47,-29,-47,-55v0,-49,36,-61,77,-68v29,-5,27,-49,-3,-47v-17,0,-24,12,-23,31xm107,-97v-19,14,-49,12,-49,41v0,15,5,27,18,27v13,0,31,-8,31,-30r0,-38",
            w: 173,
            k: {
                w: -4
            }
        },
        b: {
            d: "17,0r0,-257r49,0v1,26,-2,57,1,81v10,-15,23,-23,42,-23v41,0,59,30,59,102v0,72,-18,102,-59,102v-21,1,-32,-10,-45,-26r0,21r-47,0xm66,-97v0,42,5,64,27,64v22,0,25,-22,25,-64v0,-42,-3,-64,-25,-64v-22,0,-27,22,-27,64",
            w: 180
        },
        c: {
            d: "157,-124r-47,0v0,-19,-4,-39,-23,-39v-24,0,-28,22,-28,67v0,48,5,67,27,67v17,0,24,-13,24,-45r47,0v0,51,-20,79,-73,79v-50,0,-75,-24,-75,-102v0,-80,33,-102,79,-102v46,0,69,29,69,75",
            w: 166,
            k: {
                y: 4,
                l: 7
            }
        },
        d: {
            d: "116,0v-1,-6,2,-16,-1,-21v-11,18,-24,26,-44,26v-41,0,-59,-30,-59,-102v0,-72,18,-102,59,-102v20,-1,31,10,43,23r0,-81r49,0r0,257r-47,0xm62,-97v0,42,3,64,25,64v22,0,27,-22,27,-64v0,-42,-5,-64,-27,-64v-22,0,-25,22,-25,64",
            w: 180
        },
        e: {
            d: "157,-89r-98,0v-1,28,3,60,25,60v17,0,23,-12,26,-36r44,0v-2,45,-22,70,-70,70v-72,0,-75,-56,-75,-104v0,-52,10,-100,77,-100v62,-1,72,46,71,110xm59,-119r51,0v-1,-30,-4,-46,-25,-46v-22,1,-27,24,-26,46",
            w: 166,
            k: {
                x: 4
            }
        },
        f: {
            d: "27,0r0,-160r-25,0r0,-34r25,0v-7,-57,24,-75,79,-67r0,35v-25,-3,-33,7,-29,32r29,0r0,34r-29,0r0,160r-50,0",
            w: 106,
            k: {
                a: 7
            }
        },
        g: {
            d: "62,-91v0,27,4,51,25,51v21,0,27,-23,27,-56v0,-47,-6,-65,-26,-65v-22,0,-26,18,-26,70xm63,14v-1,12,12,21,24,21v29,2,30,-34,26,-61v-42,49,-100,23,-100,-74v0,-45,6,-99,60,-99v15,-1,29,9,41,27r0,-22r48,0r0,184v0,51,-23,76,-81,76v-42,0,-66,-19,-66,-52r48,0",
            w: 180,
            k: {
                y: -4
            }
        },
        h: {
            d: "17,0r0,-257r49,0r1,83v23,-40,96,-35,96,30r0,144r-49,0r0,-134v0,-20,-6,-27,-21,-27v-18,0,-27,11,-27,32r0,129r-49,0",
            w: 180
        },
        i: {
            d: "18,-217r0,-43r50,0r0,43r-50,0xm18,0r0,-194r50,0r0,194r-50,0",
            w: 86
        },
        j: {
            d: "18,-217r0,-43r50,0r0,43r-50,0xm18,-4r0,-190r50,0r0,207v4,40,-28,54,-77,50r0,-36v23,2,27,-5,27,-31",
            w: 86
        },
        k: {
            d: "18,-257r50,0r1,136r50,-73r55,0r-56,77r64,117r-55,0r-41,-80r-18,23r0,57r-50,0r0,-257",
            w: 180
        },
        l: {
            d: "18,0r0,-257r50,0r0,257r-50,0",
            w: 86
        },
        m: {
            d: "17,0r0,-194r47,0v1,6,-2,16,1,20v21,-36,83,-32,94,6v18,-49,97,-40,97,27r0,141r-49,0r0,-135v0,-16,-6,-26,-20,-26v-16,0,-26,11,-26,34r0,127r-49,0r0,-135v0,-16,-6,-26,-20,-26v-16,0,-26,11,-26,34r0,127r-49,0",
            w: 272
        },
        n: {
            d: "17,0r0,-194r47,0v1,7,-2,18,1,23v22,-43,98,-40,98,27r0,144r-49,0r0,-134v0,-20,-6,-27,-21,-27v-18,0,-27,11,-27,32r0,129r-49,0",
            w: 180,
            k: {
                y: -4
            }
        },
        o: {
            d: "10,-97v0,-66,19,-102,76,-102v59,0,77,34,77,102v0,66,-20,102,-77,102v-59,0,-76,-34,-76,-102xm60,-97v0,40,2,68,26,68v20,0,27,-19,27,-68v0,-49,-7,-68,-27,-68v-24,0,-26,28,-26,68",
            k: {
                v: -4
            }
        },
        p: {
            d: "17,63r0,-257r47,0v1,6,-2,16,1,21v11,-18,24,-26,44,-26v41,0,59,30,59,102v0,72,-18,102,-59,102v-20,1,-31,-10,-43,-23r0,81r-49,0xm66,-97v0,42,5,64,27,64v22,0,25,-22,25,-64v0,-42,-3,-64,-25,-64v-22,0,-27,22,-27,64",
            w: 180,
            k: {
                ".": 6,
                ",": 6
            }
        },
        q: {
            d: "114,63v-1,-26,2,-57,-1,-81v-10,15,-23,23,-42,23v-41,0,-59,-30,-59,-102v0,-72,18,-102,59,-102v21,-1,32,10,45,26r0,-21r47,0r0,257r-49,0xm114,-97v0,-42,-5,-64,-27,-64v-22,0,-25,22,-25,64v0,42,3,64,25,64v22,0,27,-22,27,-64",
            w: 180
        },
        r: {
            d: "17,0r0,-194r47,0v1,8,-2,20,1,26v11,-21,26,-35,53,-30r0,48v-27,-4,-52,2,-52,34r0,116r-49,0",
            w: 119,
            k: {
                y: -6,
                v: -6,
                q: 6,
                ".": 27,
                "-": 13,
                ",": 27
            }
        },
        s: {
            d: "8,-63r45,0v-1,21,7,33,26,34v30,2,32,-39,7,-45v-35,-8,-76,-24,-76,-66v0,-32,20,-59,71,-59v48,1,68,21,66,63r-44,0v6,-37,-44,-37,-46,-8v-2,20,34,28,52,33v73,20,46,122,-29,116v-58,-4,-73,-26,-72,-68",
            w: 159
        },
        t: {
            d: "27,-194r0,-55r50,0r0,55r29,0r0,34r-29,0r0,105v-2,20,12,23,29,20r0,35v-45,5,-79,6,-79,-49r0,-111r-25,0r0,-34r25,0",
            w: 106
        },
        u: {
            d: "116,0v-1,-7,2,-18,-1,-23v-22,43,-98,40,-98,-27r0,-144r49,0r0,134v0,20,6,27,21,27v18,0,27,-11,27,-32r0,-129r49,0r0,194r-47,0",
            w: 180
        },
        v: {
            d: "81,-55v12,-44,17,-94,27,-139r50,0r-49,194r-57,0r-50,-194r52,0",
            w: 159,
            k: {
                a: 6,
                ".": 20,
                ",": 20
            }
        },
        w: {
            d: "45,0r-42,-194r49,0r24,138r24,-138r54,0r26,138r23,-138r48,0r-42,194r-56,0r-27,-138r-24,138r-57,0",
            w: 253,
            k: {
                ".": 13,
                ",": 13
            }
        },
        x: {
            d: "83,-135r27,-59r52,0r-51,94r53,100r-52,0r-29,-63r-29,63r-51,0r52,-100r-50,-94r52,0",
            w: 166
        },
        y: {
            d: "17,63r0,-37v23,5,39,-4,39,-26r-54,-194r52,0r29,134r25,-134r50,0r-47,181v-19,76,-31,77,-94,76",
            w: 159,
            k: {
                a: 6,
                ".": 20,
                ",": 20
            }
        },
        z: {
            d: "9,0r0,-38r77,-115r-74,0r0,-41r133,0r0,38r-78,116r78,0r0,40r-136,0",
            w: 153
        },
        "{": {
            d: "11,-81r0,-29v55,-5,-10,-150,61,-147r32,0r0,36v-57,-7,6,112,-51,126v33,5,26,60,26,99v-1,19,5,29,25,26r0,36v-43,3,-69,-2,-69,-48v0,-37,10,-97,-24,-99",
            w: 113
        },
        "|": {
            d: "18,5r0,-267r44,0r0,267r-44,0",
            w: 79
        },
        "}": {
            d: "9,66r0,-36v57,7,-5,-112,51,-126v-33,-4,-26,-60,-26,-99v0,-19,-5,-29,-25,-26r0,-36v43,-3,70,2,70,48v0,37,-11,97,24,99r0,29v-54,6,10,150,-62,147r-32,0",
            w: 113
        },
        "~": {
            d: "69,-126v24,0,61,25,77,25v14,0,28,-16,32,-25r13,39v-10,18,-23,30,-44,30v-25,0,-61,-24,-77,-24v-14,0,-28,15,-32,24r-13,-38v10,-18,23,-31,44,-31",
            w: 216
        },
        "'": {
            d: "24,-141r0,-116r45,0r0,116r-45,0",
            w: 93
        },
        "`": {
            d: "63,-217r-33,0r-36,-52r50,0",
            w: 79
        },
        "ı": {
            d: "18,0r0,-194r50,0r0,194r-50,0",
            w: 86
        },
        "Ç": {
            d: "88,4v-66,-5,-75,-60,-75,-133v0,-78,9,-133,87,-133v74,0,80,55,80,92r-52,0v0,-37,-8,-54,-31,-54v-26,0,-33,26,-33,99v0,78,12,92,34,92v18,0,32,-9,32,-65r52,0v1,55,-17,102,-76,103r-10,16v20,-6,43,3,42,24v7,34,-58,39,-81,26r6,-16v10,6,43,9,43,-7v0,-18,-31,-3,-36,-16",
            w: 193,
            k: {
                A: 6
            }
        },
        "Ö": {
            d: "13,-129v0,-78,9,-133,87,-133v78,0,88,55,88,133v0,78,-10,134,-88,134v-78,0,-87,-56,-87,-134xm64,-129v0,69,5,96,36,96v31,0,36,-27,36,-96v0,-69,-5,-95,-36,-95v-31,0,-36,26,-36,95xm111,-277r0,-42r41,0r0,42r-41,0xm49,-277r0,-42r41,0r0,42r-41,0",
            w: 200,
            k: {
                Y: 9,
                T: 9,
                A: 9,
                ".": 9,
                ",": 9
            }
        },
        "Ü": {
            d: "15,-257r52,0r0,179v0,26,6,45,30,45v24,0,30,-19,30,-45r0,-179r52,0r0,179v0,65,-40,83,-82,83v-42,0,-82,-14,-82,-83r0,-179xm108,-277r0,-42r40,0r0,42r-40,0xm46,-277r0,-42r40,0r0,42r-40,0",
            w: 193
        },
        "ç": {
            d: "75,5v-50,-5,-66,-32,-66,-102v0,-80,33,-102,79,-102v46,0,69,29,69,75r-47,0v0,-19,-4,-39,-23,-39v-24,0,-28,22,-28,67v0,48,5,67,27,67v17,0,24,-13,24,-45r47,0v0,48,-18,76,-63,79r-10,16v19,-6,42,3,42,24v6,34,-59,39,-82,26r7,-16v10,6,43,9,43,-7v0,-18,-31,-3,-36,-16",
            w: 166,
            k: {
                y: 4,
                l: 7
            }
        },
        "ö": {
            d: "10,-97v0,-66,19,-102,76,-102v59,0,77,34,77,102v0,66,-20,102,-77,102v-59,0,-76,-34,-76,-102xm60,-97v0,40,2,68,26,68v20,0,27,-19,27,-68v0,-49,-7,-68,-27,-68v-24,0,-26,28,-26,68xm97,-221r0,-43r41,0r0,43r-41,0xm35,-221r0,-43r41,0r0,43r-41,0",
            k: {
                v: -4
            }
        },
        "ü": {
            d: "116,0v-1,-7,2,-18,-1,-23v-22,43,-98,40,-98,-27r0,-144r49,0r0,134v0,20,6,27,21,27v18,0,27,-11,27,-32r0,-129r49,0r0,194r-47,0xm101,-221r0,-43r40,0r0,43r-40,0xm39,-221r0,-43r40,0r0,43r-40,0",
            w: 180
        },
        "Ğ": {
            d: "182,-181r-50,0v0,-19,-8,-43,-29,-43v-25,0,-36,26,-36,90v0,62,6,101,36,101v21,0,38,-32,32,-67r-34,0r0,-39r84,0r0,139r-38,0v-1,-7,2,-18,-1,-24v-13,20,-30,29,-54,29v-64,0,-77,-47,-77,-136v0,-86,21,-131,87,-131v57,0,80,29,80,81xm52,-324r24,0v1,31,52,31,53,0r23,0v0,19,-11,51,-50,51v-39,0,-50,-32,-50,-51",
            w: 200
        },
        "İ": {
            d: "72,0r-51,0r0,-257r51,0r0,257xm26,-319r41,0r0,42r-41,0r0,-42",
            w: 92
        },
        "Ş": {
            d: "77,5v-50,-3,-71,-32,-69,-85r52,0v-1,28,4,47,34,47v16,0,31,-10,31,-32v0,-23,-11,-32,-46,-44v-46,-15,-67,-35,-67,-77v0,-50,30,-76,79,-76v48,0,83,22,80,76r-50,0v0,-24,-8,-38,-28,-38v-42,0,-40,52,-3,65v40,14,89,37,89,85v0,53,-33,77,-84,79r-10,16v20,-6,43,3,42,24v7,34,-58,39,-81,26r7,-16v10,6,42,9,42,-7v1,-19,-31,-3,-36,-16",
            w: 186
        },
        "": {
            d: "77,5v-50,-3,-71,-32,-69,-85r52,0v-1,28,4,47,34,47v16,0,31,-10,31,-32v0,-23,-11,-32,-46,-44v-46,-15,-67,-35,-67,-77v0,-50,30,-76,79,-76v48,0,83,22,80,76r-50,0v0,-24,-8,-38,-28,-38v-42,0,-40,52,-3,65v40,14,89,37,89,85v0,53,-33,77,-84,79r-10,16v20,-6,43,3,42,24v7,34,-58,39,-81,26r7,-16v10,6,42,9,42,-7v1,-19,-31,-3,-36,-16",
            w: 186
        },
        "ğ": {
            d: "62,-91v0,27,4,51,25,51v21,0,27,-23,27,-56v0,-47,-6,-65,-26,-65v-22,0,-26,18,-26,70xm63,14v-1,12,12,21,24,21v29,2,30,-34,26,-61v-42,49,-100,23,-100,-74v0,-45,6,-99,60,-99v15,-1,29,9,41,27r0,-22r48,0r0,184v0,51,-23,76,-81,76v-42,0,-66,-19,-66,-52r48,0xm41,-268r23,0v1,30,53,30,54,0r23,0v0,19,-11,51,-50,51v-39,0,-50,-32,-50,-51",
            w: 180,
            k: {
                y: -4
            }
        },
        "ş": {
            d: "65,4v-41,-2,-60,-28,-57,-67r45,0v-1,21,7,33,26,34v30,2,32,-39,7,-45v-35,-8,-76,-24,-76,-66v0,-32,20,-59,71,-59v48,1,68,21,66,63r-44,0v6,-37,-44,-37,-46,-8v-2,20,35,26,52,33v31,10,43,26,43,55v0,39,-29,59,-68,61r-11,16v20,-6,43,3,42,24v7,34,-58,39,-81,26r7,-16v10,6,43,9,43,-7v0,-18,-32,-3,-36,-16",
            w: 159
        },
        "": {
            d: "65,4v-41,-2,-60,-28,-57,-67r45,0v-1,21,7,33,26,34v30,2,32,-39,7,-45v-35,-8,-76,-24,-76,-66v0,-32,20,-59,71,-59v48,1,68,21,66,63r-44,0v6,-37,-44,-37,-46,-8v-2,20,35,26,52,33v31,10,43,26,43,55v0,39,-29,59,-68,61r-11,16v20,-6,43,3,42,24v7,34,-58,39,-81,26r7,-16v10,6,43,9,43,-7v0,-18,-32,-3,-36,-16",
            w: 159
        },
        " ": {
            w: 86
        }
    }
}), Cufon.registerFont({
    w: 138,
    face: {
        "font-family": "MyriadPro-Condensed",
        "font-weight": 400,
        "font-stretch": "condensed",
        "units-per-em": "360",
        "panose-1": "2 11 5 6 3 4 3 2 2 4",
        ascent: "270",
        descent: "-90",
        bbox: "-10 -294 228 90",
        "underline-thickness": "18",
        "underline-position": "-18",
        stemh: "21",
        stemv: "28",
        "unicode-range": "U+0020-U+015F"
    },
    glyphs: {
        " ": {
            w: 56,
            k: {
                T: 13,
                V: 13,
                W: 13,
                Y: 13
            }
        },
        "!": {
            d: "48,-67r-23,0r-3,-176r30,0xm36,3v-10,0,-17,-8,-17,-20v0,-12,8,-19,18,-19v11,0,17,7,17,19v0,12,-6,20,-18,20",
            w: 73
        },
        '"': {
            d: "18,-246r26,0r-5,84r-16,0xm68,-246r26,0r-5,84r-16,0",
            w: 111,
            k: {
                T: -2,
                J: 12,
                C: 1,
                G: 1,
                O: 1,
                Q: 1,
                "Ç": 1,
                "Ö": 1,
                "Ğ": 1,
                V: -2,
                W: -2,
                X: -2,
                Y: -4,
                A: 22,
                f: -5,
                g: 8,
                "ğ": 8,
                c: 8,
                d: 8,
                e: 8,
                o: 8,
                q: 8,
                "ç": 8,
                "ö": 8,
                s: 1,
                "ş": 1,
                t: -5,
                x: -1,
                ",": 33,
                ".": 33
            }
        },
        "#": {
            d: "52,-91r28,0r7,-54r-28,0xm41,0r-19,0r9,-72r-23,0r0,-19r26,0r7,-54r-25,0r0,-19r28,0r9,-70r18,0r-9,70r28,0r9,-70r18,0r-9,70r23,0r0,19r-26,0r-6,54r24,0r0,19r-27,0r-9,72r-19,0r9,-72r-27,0"
        },
        $: {
            d: "95,-64v0,-50,-76,-52,-76,-106v0,-29,18,-50,41,-56r0,-35r20,0r0,33v15,0,28,4,34,9r-6,22v-17,-16,-62,-9,-62,22v0,21,10,30,34,44v28,17,41,36,41,63v0,34,-19,55,-43,60r0,38r-20,0r0,-36v-17,0,-31,-7,-40,-12r7,-21v24,18,70,14,70,-25"
        },
        "%": {
            d: "55,-237v26,0,44,28,44,71v0,49,-22,71,-44,71v-26,0,-44,-27,-44,-71v0,-45,19,-71,44,-71xm54,-220v-32,4,-30,105,1,108v32,-4,32,-104,-1,-108xm62,5r-17,0r124,-244r18,0xm178,-139v26,0,43,28,43,71v0,49,-22,71,-44,71v-26,0,-44,-27,-44,-71v0,-45,20,-71,45,-71xm177,-122v-32,3,-32,104,0,107v32,-3,32,-103,0,-107",
            w: 232
        },
        "&": {
            d: "170,0r-30,0v-4,-6,-9,-11,-17,-23v-34,50,-113,20,-113,-41v0,-28,14,-50,38,-71v-29,-37,-23,-110,32,-111v22,0,42,17,42,50v1,25,-12,44,-41,67v9,15,27,44,46,69v10,-22,13,-53,14,-70r25,0v-3,29,-11,64,-27,88xm37,-67v0,45,53,66,74,27v-21,-28,-38,-54,-52,-77v-11,10,-22,28,-22,50xm77,-225v-30,0,-26,58,-8,78v19,-14,29,-28,29,-47v0,-15,-5,-31,-21,-31",
            w: 176
        },
        "(": {
            d: "60,-249r22,0v-47,56,-48,235,0,292r-22,0v-19,-27,-39,-75,-39,-146v0,-71,20,-119,39,-146",
            w: 91,
            k: {
                T: -4,
                J: -3,
                C: 7,
                G: 7,
                O: 7,
                Q: 7,
                "Ç": 7,
                "Ö": 7,
                "Ğ": 7,
                V: -8,
                W: -8,
                X: -1,
                Y: -10,
                A: 6,
                j: -11,
                c: 4,
                d: 4,
                e: 4,
                o: 4,
                q: 4,
                "ç": 4,
                "ö": 4
            }
        },
        ")": {
            d: "31,43r-22,0v47,-56,48,-237,0,-292r22,0v19,27,39,75,39,146v0,71,-20,118,-39,146",
            w: 91
        },
        "*": {
            d: "88,-246r19,11v-9,15,-25,30,-30,44r48,-6r0,22v-16,-1,-35,-6,-49,-5r32,41r-20,12r-21,-48r-23,49r-18,-12v9,-14,24,-30,30,-43r-47,6r0,-22v15,1,34,6,47,5r-30,-42r19,-10r22,46",
            w: 134
        },
        "+": {
            d: "97,-192r21,0r0,86r83,0r0,20r-83,0r0,86r-21,0r0,-86r-82,0r0,-20r82,0r0,-86",
            w: 214
        },
        ",": {
            d: "25,43r-19,2v8,-21,17,-59,21,-84r31,-3v-8,29,-23,69,-33,85",
            w: 69,
            k: {
                '"': 26,
                "'": 26
            }
        },
        "-": {
            d: "11,-105r79,0r0,21r-79,0r0,-21",
            w: 100,
            k: {
                T: 13,
                J: 6,
                C: -2,
                G: -2,
                O: -2,
                Q: -2,
                "Ç": -2,
                "Ö": -2,
                "Ğ": -2,
                V: 4,
                W: 4,
                X: 5,
                Y: 13,
                A: 3,
                g: -5,
                "ğ": -5,
                c: -7,
                d: -7,
                e: -7,
                o: -7,
                q: -7,
                "ç": -7,
                "ö": -7,
                x: 3
            }
        },
        ".": {
            d: "36,3v-10,0,-18,-8,-18,-21v0,-12,8,-21,18,-21v11,0,18,8,18,21v0,13,-6,21,-18,21",
            w: 69,
            k: {
                '"': 26,
                "'": 26
            }
        },
        "/": {
            d: "24,14r-21,0r77,-260r21,0",
            w: 101
        },
        "0": {
            d: "71,-237v38,0,58,45,58,120v0,85,-27,120,-61,120v-36,0,-58,-44,-58,-120v0,-79,23,-120,61,-120xm70,-213v-23,0,-33,39,-33,96v0,57,10,96,33,96v22,0,31,-40,31,-96v0,-55,-9,-96,-31,-96"
        },
        "1": {
            d: "68,0r-1,-206r-39,22r-5,-21v24,-10,35,-32,72,-29r0,234r-27,0"
        },
        "2": {
            d: "124,0r-112,0v-3,-24,10,-32,19,-45v27,-40,61,-82,61,-126v0,-47,-48,-49,-67,-29r-7,-21v8,-7,25,-16,46,-16v39,0,56,29,56,64v1,53,-45,105,-74,149r78,0r0,24"
        },
        "3": {
            d: "10,-8r6,-22v27,19,77,10,74,-37v-2,-33,-21,-50,-53,-48r0,-21v23,1,45,-11,47,-42v2,-40,-39,-43,-60,-25r-6,-21v31,-27,94,-9,94,41v0,24,-14,44,-35,56v21,6,41,24,41,61v0,55,-63,87,-108,58"
        },
        "4": {
            d: "109,0r-25,0r0,-65r-78,0r0,-19r76,-150r27,0r0,147r26,0r0,22r-26,0r0,65xm33,-88v14,3,35,0,51,1r1,-112"
        },
        "5": {
            d: "47,-147v41,-5,73,25,73,70v0,64,-60,99,-111,69r6,-21v33,19,79,7,78,-45v-1,-44,-32,-57,-72,-52r15,-108r84,0r0,24r-63,0"
        },
        "6": {
            d: "115,-237r0,24v-51,1,-73,48,-76,86v31,-43,90,-19,90,50v0,44,-20,80,-57,80v-79,2,-71,-157,-32,-201v20,-22,41,-38,75,-39xm70,-128v-15,0,-32,14,-32,39v0,48,15,70,34,70v15,0,30,-15,30,-56v0,-35,-13,-53,-32,-53"
        },
        "7": {
            d: "13,-234r113,0r0,17r-76,217r-27,0r75,-210r-85,0r0,-24"
        },
        "8": {
            d: "49,-124v-49,-26,-35,-116,23,-113v60,4,66,84,19,111v55,25,48,130,-23,129v-39,0,-58,-31,-58,-60v0,-33,20,-54,39,-67xm69,-18v17,0,32,-12,32,-42v0,-27,-15,-42,-33,-54v-38,15,-44,96,1,96xm70,-216v-15,0,-26,15,-26,36v0,24,15,36,29,45v28,-13,34,-80,-3,-81"
        },
        "9": {
            d: "24,3r0,-24v50,-3,71,-38,76,-87v-31,41,-89,14,-89,-50v0,-43,23,-79,59,-79v79,2,67,159,28,202v-20,22,-39,38,-74,38xm70,-109v16,-1,32,-13,31,-36v0,-49,-14,-70,-33,-70v-14,0,-30,15,-30,55v0,34,13,51,32,51"
        },
        ":": {
            d: "36,-129v-10,0,-18,-8,-18,-20v0,-12,8,-21,18,-21v11,0,18,9,18,21v0,12,-6,20,-18,20xm36,3v-10,0,-18,-8,-18,-20v0,-12,8,-21,18,-21v11,0,18,9,18,21v0,12,-6,20,-18,20",
            w: 69
        },
        ";": {
            d: "25,43r-19,2v7,-21,17,-59,21,-84r31,-3v-8,29,-23,69,-33,85xm40,-129v-10,0,-18,-8,-18,-20v0,-12,8,-21,18,-21v11,0,18,9,18,21v0,12,-6,20,-18,20",
            w: 69
        },
        "<": {
            d: "24,-87r0,-17r167,-88r0,25r-140,72r140,71r0,24",
            w: 214
        },
        "=": {
            d: "200,-121r-186,0r0,-20r186,0r0,20xm200,-53r-186,0r0,-20r186,0r0,20",
            w: 214
        },
        ">": {
            d: "191,-106r0,21r-167,85r0,-24r144,-72r-144,-72r0,-24",
            w: 214
        },
        "?": {
            d: "102,-197v-1,49,-51,73,-40,131v-9,-2,-26,5,-25,-6v-13,-39,35,-87,36,-122v1,-32,-32,-34,-51,-22r-6,-22v6,-4,20,-9,35,-9v36,0,51,24,51,50xm48,3v-10,0,-17,-8,-17,-20v0,-12,8,-20,18,-20v11,0,18,8,18,20v0,12,-7,20,-19,20",
            w: 111
        },
        "@": {
            d: "91,-41v32,-4,38,-62,45,-96v-37,-8,-57,35,-59,74v0,16,6,22,14,22xm147,10r5,13v-67,34,-139,3,-139,-90v0,-80,48,-147,113,-147v49,0,76,37,76,87v0,68,-24,104,-56,104v-14,0,-20,-13,-22,-29v-12,22,-27,29,-39,29v-17,0,-29,-14,-29,-40v0,-47,42,-109,102,-86r-14,73v-5,26,-2,37,8,37v17,0,33,-33,33,-82v0,-49,-19,-79,-61,-79v-50,0,-95,55,-95,133v0,79,62,107,118,77",
            w: 212
        },
        A: {
            d: "105,-73r-54,0r-16,73r-27,0r55,-243r31,0r54,243r-27,0xm55,-95r46,0r-24,-117",
            w: 156,
            k: {
                T: 22,
                J: -5,
                M: 3,
                C: 7,
                G: 7,
                O: 7,
                Q: 7,
                "Ç": 7,
                "Ö": 7,
                "Ğ": 7,
                U: 6,
                "Ü": 6,
                V: 17,
                W: 17,
                X: 3,
                Y: 19,
                Z: -3,
                f: 4,
                g: 3,
                "ğ": 3,
                b: 1,
                h: 1,
                k: 1,
                l: 1,
                j: 1,
                i: 1,
                m: 1,
                n: 1,
                p: 1,
                r: 1,
                "ı": 1,
                c: 4,
                d: 4,
                e: 4,
                o: 4,
                q: 4,
                "ç": 4,
                "ö": 4,
                s: -1,
                "ş": -1,
                t: 4,
                u: 2,
                "ü": 2,
                v: 9,
                w: 9,
                y: 9,
                x: -1,
                z: -4,
                ")": 3,
                "]": 3,
                "}": 3,
                '"': 20,
                "'": 20
            }
        },
        B: {
            d: "94,-132v73,24,51,133,-36,133v-18,0,-28,-1,-36,-2r0,-238v50,-14,110,-1,110,54v0,25,-14,44,-38,53xm49,-220r0,80v33,3,52,-12,55,-43v2,-27,-24,-45,-55,-37xm49,-118r0,96v34,6,63,-11,62,-47v-1,-38,-26,-52,-62,-49",
            w: 149,
            k: {
                T: 3,
                U: -1,
                "Ü": -1,
                V: 2,
                W: 2,
                Y: 3,
                b: -1,
                h: -1,
                k: -1,
                l: -1,
                i: -1,
                m: -1,
                n: -1,
                p: -1,
                r: -1,
                "ı": -1,
                c: -3,
                d: -3,
                e: -3,
                o: -3,
                q: -3,
                "ç": -3,
                "ö": -3,
                u: -1,
                "ü": -1,
                v: 1,
                w: 1,
                y: 1,
                a: -2,
                ",": 6,
                ".": 6
            }
        },
        C: {
            d: "123,-27r5,23v-69,21,-116,-16,-116,-114v0,-110,52,-140,115,-121r-6,24v-47,-16,-80,8,-80,94v0,87,31,110,82,94",
            w: 134,
            k: {
                T: -3,
                J: -5,
                C: 6,
                G: 6,
                O: 6,
                Q: 6,
                "Ç": 6,
                "Ö": 6,
                "Ğ": 6,
                V: -3,
                W: -3,
                X: -4,
                Y: -6,
                A: -2,
                b: 1,
                h: 1,
                k: 1,
                l: 1,
                i: 1,
                m: 1,
                n: 1,
                p: 1,
                r: 1,
                "ı": 1,
                c: 6,
                d: 6,
                e: 6,
                o: 6,
                q: 6,
                "ç": 6,
                "ö": 6,
                u: 6,
                "ü": 6,
                v: 13,
                w: 13,
                y: 13,
                z: -5,
                a: 4,
                ")": -2,
                "]": -2,
                "}": -2,
                '"': -3,
                "'": -3
            }
        },
        D: {
            d: "149,-127v-1,102,-37,135,-127,126r0,-238v83,-17,128,19,127,112xm49,-220r0,198v45,2,71,-16,71,-104v0,-67,-20,-101,-71,-94",
            w: 160,
            k: {
                T: 9,
                J: 3,
                V: 3,
                W: 3,
                X: 4,
                Y: 7,
                A: 7,
                f: -3,
                g: -3,
                "ğ": -3,
                b: -1,
                h: -1,
                k: -1,
                l: -1,
                i: -2,
                m: -2,
                n: -2,
                p: -2,
                r: -2,
                "ı": -2,
                c: -3,
                d: -3,
                e: -3,
                o: -3,
                q: -3,
                "ç": -3,
                "ö": -3,
                t: -3,
                u: -3,
                "ü": -3,
                v: -3,
                w: -3,
                y: -3,
                x: 1,
                z: 3,
                "-": -2,
                ")": 2,
                "]": 2,
                "}": 2,
                '"': 1,
                "'": 1,
                ",": 15,
                ".": 15
            }
        },
        E: {
            d: "110,-139r0,24r-61,0r0,91r69,0r0,24r-96,0r0,-243r92,0r0,24r-65,0r0,80r61,0",
            w: 127,
            k: {
                J: -4,
                V: -3,
                W: -3,
                Y: -4,
                f: 4,
                g: 5,
                "ğ": 5,
                b: 3,
                h: 3,
                k: 3,
                l: 3,
                i: 1,
                m: 1,
                n: 1,
                p: 1,
                r: 1,
                "ı": 1,
                c: 5,
                d: 5,
                e: 5,
                o: 5,
                q: 5,
                "ç": 5,
                "ö": 5,
                t: 1,
                u: 5,
                "ü": 5,
                v: 5,
                w: 5,
                y: 5,
                z: -1,
                a: 1,
                ",": -1,
                ".": -1
            }
        },
        F: {
            d: "22,0r0,-243r92,0r0,24r-65,0r0,85r61,0r0,24r-61,0r0,110r-27,0",
            w: 124,
            k: {
                J: 17,
                M: 3,
                A: 17,
                g: 3,
                "ğ": 3,
                b: 4,
                h: 4,
                k: 4,
                l: 4,
                i: 4,
                m: 4,
                n: 4,
                p: 4,
                r: 4,
                "ı": 4,
                c: 7,
                d: 7,
                e: 7,
                o: 7,
                q: 7,
                "ç": 7,
                "ö": 7,
                t: 1,
                u: 5,
                "ü": 5,
                v: 5,
                w: 5,
                y: 5,
                a: 13,
                ":": 5,
                ";": 5,
                ",": 31,
                ".": 31
            }
        },
        G: {
            d: "141,-6v-76,28,-134,-23,-129,-112v6,-111,57,-141,125,-120r-6,24v-51,-18,-91,4,-91,93v0,84,28,105,75,97r0,-80r-31,0r0,-23r57,0r0,121",
            w: 156,
            k: {
                b: -1,
                h: -1,
                k: -1,
                l: -1,
                i: -1,
                m: -1,
                n: -1,
                p: -1,
                r: -1,
                "ı": -1,
                c: -1,
                d: -1,
                e: -1,
                o: -1,
                q: -1,
                "ç": -1,
                "ö": -1,
                u: -1,
                "ü": -1,
                v: 3,
                w: 3,
                y: 3,
                a: -2
            }
        },
        H: {
            d: "22,-243r27,0r0,104r68,0r0,-104r28,0r0,243r-28,0r0,-113r-68,0r0,113r-27,0r0,-243",
            w: 166,
            k: {
                j: 1,
                c: 1,
                d: 1,
                e: 1,
                o: 1,
                q: 1,
                "ç": 1,
                "ö": 1,
                s: 1,
                "ş": 1,
                v: 1,
                w: 1,
                y: 1,
                x: 1,
                z: -1
            }
        },
        I: {
            d: "22,-243r27,0r0,243r-27,0r0,-243",
            w: 71,
            k: {
                j: 1,
                c: 1,
                d: 1,
                e: 1,
                o: 1,
                q: 1,
                "ç": 1,
                "ö": 1,
                s: 1,
                "ş": 1,
                v: 1,
                w: 1,
                y: 1,
                x: 1,
                z: -1
            }
        },
        J: {
            d: "48,-68r0,-175r28,0r0,175v0,62,-31,78,-75,68r3,-23v29,3,43,1,44,-45",
            w: 95,
            k: {
                i: -1,
                m: -1,
                n: -1,
                p: -1,
                r: -1,
                "ı": -1,
                u: -1,
                "ü": -1,
                v: -1,
                w: -1,
                y: -1,
                ",": 2,
                ".": 2
            }
        },
        K: {
            d: "22,0r0,-243r27,0r1,116r61,-116r30,0r-59,104r61,139r-30,0r-50,-117r-14,23r0,94r-27,0",
            w: 145,
            k: {
                J: -4,
                C: 8,
                G: 8,
                O: 8,
                Q: 8,
                "Ç": 8,
                "Ö": 8,
                "Ğ": 8,
                V: -1,
                W: -1,
                Y: 3,
                Z: -3,
                A: -3,
                g: 2,
                "ğ": 2,
                b: -1,
                h: -1,
                k: -1,
                l: -1,
                c: 4,
                d: 4,
                e: 4,
                o: 4,
                q: 4,
                "ç": 4,
                "ö": 4,
                u: 5,
                "ü": 5,
                v: 9,
                w: 9,
                y: 9,
                a: -1,
                ":": -3,
                ";": -3,
                "-": 9,
                '"': -2,
                "'": -2,
                ",": -3,
                ".": -3
            }
        },
        L: {
            d: "22,0r0,-243r27,0r0,220r68,0r0,23r-95,0",
            w: 123,
            k: {
                T: 24,
                J: -5,
                C: 15,
                G: 15,
                O: 15,
                Q: 15,
                "Ç": 15,
                "Ö": 15,
                "Ğ": 15,
                U: 11,
                "Ü": 11,
                V: 22,
                W: 22,
                Y: 28,
                A: -1,
                f: 1,
                g: 3,
                "ğ": 3,
                c: 6,
                d: 6,
                e: 6,
                o: 6,
                q: 6,
                "ç": 6,
                "ö": 6,
                t: 6,
                u: 6,
                "ü": 6,
                v: 8,
                w: 8,
                y: 8,
                a: 1,
                "-": 16,
                '"': 26,
                "'": 26
            }
        },
        M: {
            d: "174,0r-6,-104v-3,-31,-1,-71,-3,-95r-49,198r-18,0r-50,-198r-7,199r-25,0r14,-243r30,0r49,195v11,-63,30,-133,45,-195r31,0r14,243r-25,0",
            w: 215,
            k: {
                T: 9,
                V: 3,
                W: 3,
                Y: 3,
                A: 3,
                g: 3,
                "ğ": 3,
                i: -1,
                m: -1,
                n: -1,
                p: -1,
                r: -1,
                "ı": -1,
                '"': 4,
                "'": 4
            }
        },
        N: {
            d: "47,0r-25,0r0,-243r26,0r45,114v11,25,20,61,31,85r-3,-199r25,0r0,243r-26,0r-46,-117v-11,-23,-19,-54,-30,-82v4,48,3,137,3,199",
            w: 167,
            k: {
                j: 1,
                c: 1,
                d: 1,
                e: 1,
                o: 1,
                q: 1,
                "ç": 1,
                "ö": 1,
                s: 1,
                "ş": 1,
                v: 1,
                w: 1,
                y: 1,
                x: 1,
                z: -1
            }
        },
        O: {
            d: "83,-246v41,0,66,46,66,123v0,91,-31,126,-70,126v-40,0,-67,-43,-67,-125v0,-80,29,-124,71,-124xm81,-221v-56,1,-56,199,0,199v55,-3,54,-195,0,-199",
            w: 160,
            k: {
                T: 9,
                J: 3,
                V: 3,
                W: 3,
                X: 4,
                Y: 7,
                A: 7,
                f: -3,
                g: -3,
                "ğ": -3,
                b: -1,
                h: -1,
                k: -1,
                l: -1,
                i: -2,
                m: -2,
                n: -2,
                p: -2,
                r: -2,
                "ı": -2,
                c: -3,
                d: -3,
                e: -3,
                o: -3,
                q: -3,
                "ç": -3,
                "ö": -3,
                t: -3,
                u: -3,
                "ü": -3,
                v: -3,
                w: -3,
                y: -3,
                x: 1,
                z: 3,
                "-": -2,
                ")": 2,
                "]": 2,
                "}": 2,
                '"': 1,
                "'": 1,
                ",": 15,
                ".": 15
            }
        },
        P: {
            d: "134,-176v-3,52,-34,80,-85,77r0,99r-27,0r0,-239v53,-15,115,3,112,63xm49,-219r0,97v36,3,55,-13,57,-52v2,-31,-21,-55,-57,-45",
            w: 143,
            k: {
                J: 16,
                X: 4,
                Y: 2,
                Z: 5,
                A: 21,
                g: 10,
                "ğ": 10,
                i: 2,
                m: 2,
                n: 2,
                p: 2,
                r: 2,
                "ı": 2,
                c: 9,
                d: 9,
                e: 9,
                o: 9,
                q: 9,
                "ç": 9,
                "ö": 9,
                s: 5,
                "ş": 5,
                t: -4,
                u: 3,
                "ü": 3,
                v: -3,
                w: -3,
                y: -3,
                a: 9,
                ":": 5,
                ";": 5,
                "-": 8,
                ")": 2,
                "]": 2,
                "}": 2,
                ",": 49,
                ".": 49
            }
        },
        Q: {
            d: "144,33v-23,-9,-44,-24,-64,-30v-40,0,-68,-40,-68,-125v0,-81,29,-124,71,-124v41,0,66,46,66,123v1,67,-16,102,-41,116v16,7,33,12,47,17xm81,-22v55,-2,54,-196,0,-199v-57,3,-57,197,0,199",
            w: 160,
            k: {
                T: 9,
                J: 3,
                V: 3,
                W: 3,
                X: 4,
                Y: 7,
                A: 7,
                f: -3,
                g: -3,
                "ğ": -3,
                b: -1,
                h: -1,
                k: -1,
                l: -1,
                i: -2,
                m: -2,
                n: -2,
                p: -2,
                r: -2,
                "ı": -2,
                c: -3,
                d: -3,
                e: -3,
                o: -3,
                q: -3,
                "ç": -3,
                "ö": -3,
                t: -3,
                u: -3,
                "ü": -3,
                v: -3,
                w: -3,
                y: -3,
                x: 1,
                z: 3,
                "-": -2,
                ")": 2,
                "]": 2,
                "}": 2,
                '"': 1,
                "'": 1,
                ",": 15,
                ".": 15
            }
        },
        R: {
            d: "22,-239v54,-15,111,3,111,60v0,31,-15,51,-39,63v33,-2,37,99,47,116r-28,0v-4,-7,-8,-30,-13,-60v-6,-38,-18,-47,-51,-45r0,105r-27,0r0,-239xm49,-220r0,93v35,3,55,-16,56,-49v2,-34,-20,-50,-56,-44",
            w: 147,
            k: {
                T: 4,
                J: -3,
                X: -5,
                Y: 5,
                A: -2,
                b: -3,
                h: -3,
                k: -3,
                l: -3,
                i: -3,
                m: -3,
                n: -3,
                p: -3,
                r: -3,
                "ı": -3,
                t: -4,
                a: -3,
                "-": 1
            }
        },
        S: {
            d: "91,-63v-3,-53,-77,-60,-77,-118v0,-51,57,-79,98,-55r-7,24v-20,-17,-71,-4,-64,26v0,24,13,33,36,50v29,21,42,43,42,70v0,59,-64,83,-109,58r6,-24v27,19,77,10,75,-31",
            w: 129,
            k: {
                i: -1,
                m: -1,
                n: -1,
                p: -1,
                r: -1,
                "ı": -1,
                c: -3,
                d: -3,
                e: -3,
                o: -3,
                q: -3,
                "ç": -3,
                "ö": -3,
                t: 1,
                u: -2,
                "ü": -2,
                v: 4,
                w: 4,
                y: 4,
                a: -3,
                "-": -2
            }
        },
        T: {
            d: "50,0r0,-218r-44,0r0,-25r116,0r0,25r-44,0r0,218r-28,0",
            w: 127,
            k: {
                "ı": 19,
                i: 19,
                T: -4,
                J: 9,
                M: 1,
                C: 12,
                G: 12,
                O: 12,
                Q: 12,
                "Ç": 12,
                "Ö": 12,
                "Ğ": 12,
                V: -5,
                W: -5,
                X: -5,
                Y: -7,
                A: 16,
                S: 5,
                "Ş": 5,
                g: 21,
                "ğ": 21,
                b: 5,
                h: 5,
                k: 5,
                l: 5,
                m: 19,
                n: 19,
                p: 19,
                r: 19,
                c: 22,
                d: 22,
                e: 22,
                o: 22,
                q: 22,
                "ç": 22,
                "ö": 22,
                s: 22,
                "ş": 22,
                u: 19,
                "ü": 19,
                v: 19,
                w: 19,
                y: 19,
                x: 15,
                z: 20,
                a: 19,
                ":": 14,
                ";": 14,
                "-": 13,
                ")": -7,
                "]": -7,
                "}": -7,
                '"': -2,
                "'": -2,
                ",": 26,
                ".": 26
            }
        },
        U: {
            d: "21,-243r28,0r0,163v0,44,15,58,33,58v21,0,34,-16,34,-58r0,-163r27,0r0,159v0,63,-26,87,-62,87v-36,0,-60,-24,-60,-84r0,-162",
            w: 164,
            k: {
                M: 2,
                Y: 2,
                A: 4,
                g: 1,
                "ğ": 1,
                c: 1,
                d: 1,
                e: 1,
                o: 1,
                q: 1,
                "ç": 1,
                "ö": 1,
                s: 3,
                "ş": 3,
                t: 1,
                v: 3,
                w: 3,
                y: 3,
                x: 4,
                z: 5,
                a: 2,
                ",": 10,
                ".": 10
            }
        },
        V: {
            d: "90,0r-29,0r-55,-243r30,0r41,207r40,-207r29,0",
            w: 151,
            k: {
                T: -4,
                J: 5,
                C: 3,
                G: 3,
                O: 3,
                Q: 3,
                "Ç": 3,
                "Ö": 3,
                "Ğ": 3,
                A: 15,
                g: 13,
                "ğ": 13,
                b: 3,
                h: 3,
                k: 3,
                l: 3,
                i: 9,
                m: 9,
                n: 9,
                p: 9,
                r: 9,
                "ı": 9,
                c: 13,
                d: 13,
                e: 13,
                o: 13,
                q: 13,
                "ç": 13,
                "ö": 13,
                s: 6,
                "ş": 6,
                t: 3,
                u: 10,
                "ü": 10,
                v: 6,
                w: 6,
                y: 6,
                x: 3,
                z: 5,
                a: 14,
                ":": 6,
                ";": 6,
                "-": 8,
                ")": -8,
                "]": -8,
                "}": -8,
                '"': -4,
                "'": -4,
                ",": 23,
                ".": 23
            }
        },
        W: {
            d: "80,0r-26,0r-47,-243r29,0r33,205v9,-70,26,-137,38,-205r23,0r23,114r15,91v4,-35,7,-61,12,-89r21,-116r27,0r-47,243r-28,0r-23,-118v-6,-25,-7,-55,-13,-80v-8,68,-25,133,-37,198",
            w: 235,
            k: {
                T: -4,
                J: 5,
                C: 3,
                G: 3,
                O: 3,
                Q: 3,
                "Ç": 3,
                "Ö": 3,
                "Ğ": 3,
                A: 15,
                g: 13,
                "ğ": 13,
                b: 3,
                h: 3,
                k: 3,
                l: 3,
                i: 9,
                m: 9,
                n: 9,
                p: 9,
                r: 9,
                "ı": 9,
                c: 13,
                d: 13,
                e: 13,
                o: 13,
                q: 13,
                "ç": 13,
                "ö": 13,
                s: 6,
                "ş": 6,
                t: 3,
                u: 10,
                "ü": 10,
                v: 6,
                w: 6,
                y: 6,
                x: 3,
                z: 5,
                a: 14,
                ":": 6,
                ";": 6,
                "-": 8,
                ")": -8,
                "]": -8,
                "}": -8,
                '"': -4,
                "'": -4,
                ",": 23,
                ".": 23
            }
        },
        X: {
            d: "136,0r-29,0r-38,-101r-35,101r-29,0r50,-124r-48,-119r29,0r36,95r33,-95r29,0r-48,118",
            w: 141,
            k: {
                T: -4,
                J: -5,
                C: 5,
                G: 5,
                O: 5,
                Q: 5,
                "Ç": 5,
                "Ö": 5,
                "Ğ": 5,
                V: -4,
                W: -4,
                X: 2,
                Y: -4,
                A: -3,
                i: 1,
                m: 1,
                n: 1,
                p: 1,
                r: 1,
                "ı": 1,
                c: 5,
                d: 5,
                e: 5,
                o: 5,
                q: 5,
                "ç": 5,
                "ö": 5,
                u: 4,
                "ü": 4,
                v: 9,
                w: 9,
                y: 9,
                a: 1,
                "-": 4,
                '"': -2,
                "'": -2
            }
        },
        Y: {
            d: "85,0r-28,0r0,-103r-54,-140r30,0r40,116r38,-116r29,0r-55,139r0,104",
            w: 141,
            k: {
                "ö": 21,
                T: -6,
                J: 8,
                M: 4,
                C: 10,
                G: 10,
                O: 10,
                Q: 10,
                "Ç": 10,
                "Ö": 10,
                "Ğ": 10,
                V: -9,
                W: -9,
                X: -4,
                Y: -11,
                A: 23,
                S: 4,
                "Ş": 4,
                g: 19,
                "ğ": 19,
                b: 3,
                h: 3,
                k: 3,
                l: 3,
                i: 5,
                m: 5,
                n: 5,
                p: 5,
                r: 5,
                "ı": 5,
                c: 21,
                d: 21,
                e: 21,
                o: 21,
                q: 21,
                "ç": 21,
                s: 13,
                "ş": 13,
                t: 10,
                u: 19,
                "ü": 19,
                v: 16,
                w: 16,
                y: 16,
                x: 8,
                z: 10,
                a: 24,
                ":": 17,
                ";": 17,
                "-": 14,
                ")": -9,
                "]": -9,
                "}": -9,
                '"': -2,
                "'": -2,
                ",": 32,
                ".": 32
            }
        },
        Z: {
            d: "7,0r0,-17r83,-202r-76,0r0,-24r107,0r0,19r-81,200r81,0r0,24r-114,0",
            w: 128,
            k: {
                J: -4,
                C: 6,
                G: 6,
                O: 6,
                Q: 6,
                "Ç": 6,
                "Ö": 6,
                "Ğ": 6,
                X: -3,
                Y: -3,
                A: -3,
                i: -1,
                m: -1,
                n: -1,
                p: -1,
                r: -1,
                "ı": -1,
                c: 6,
                d: 6,
                e: 6,
                o: 6,
                q: 6,
                "ç": 6,
                "ö": 6,
                u: 4,
                "ü": 4,
                v: 4,
                w: 4,
                y: 4,
                a: 2,
                "-": 5
            }
        },
        "[": {
            d: "80,40r-53,0r0,-287r53,0r0,19r-31,0r0,249r31,0r0,19",
            w: 91,
            k: {
                T: -4,
                J: -3,
                C: 7,
                G: 7,
                O: 7,
                Q: 7,
                "Ç": 7,
                "Ö": 7,
                "Ğ": 7,
                V: -8,
                W: -8,
                X: -1,
                Y: -10,
                A: 6,
                j: -11,
                c: 4,
                d: 4,
                e: 4,
                o: 4,
                q: 4,
                "ç": 4,
                "ö": 4
            }
        },
        "\\": {
            d: "92,14r-21,0r-71,-260r21,0",
            w: 94
        },
        "]": {
            d: "11,-247r54,0r0,287r-54,0r0,-19r31,0r0,-249r-31,0r0,-19",
            w: 91
        },
        "^": {
            d: "192,-68r-24,0r-61,-140r-61,140r-23,0r74,-166r20,0",
            w: 214
        },
        _: {
            d: "0,27r180,0r0,18r-180,0r0,-18",
            w: 180
        },
        a: {
            d: "62,-177v74,-1,41,107,51,177r-25,0v-1,-6,0,-14,-3,-18v-21,37,-76,21,-76,-29v0,-44,36,-63,75,-63v8,-41,-30,-56,-57,-36r-6,-19v8,-5,24,-12,41,-12xm58,-19v26,1,28,-39,26,-71v-14,0,-48,2,-48,40v0,23,12,31,22,31",
            w: 131
        },
        b: {
            d: "18,0r1,-249r27,0r1,96v9,-17,22,-24,37,-24v27,0,48,31,48,88v0,63,-26,92,-53,92v-19,1,-27,-13,-36,-26r-2,23r-23,0xm73,-153v-46,0,-32,133,1,133v22,0,30,-30,30,-67v0,-35,-8,-66,-31,-66",
            w: 143,
            k: {
                T: 16,
                v: 3,
                w: 3,
                y: 3,
                x: 5,
                z: 3,
                "-": -5,
                '"': 7,
                "'": 7,
                ",": 12,
                ".": 12
            }
        },
        c: {
            d: "100,-26r4,22v-51,20,-92,-12,-92,-82v0,-59,39,-108,93,-85r-5,22v-37,-17,-60,20,-60,63v0,50,24,76,60,60",
            w: 108,
            k: {
                T: 10,
                f: -4,
                c: 2,
                d: 2,
                e: 2,
                o: 2,
                q: 2,
                "ç": 2,
                "ö": 2,
                t: -5,
                u: -1,
                "ü": -1,
                v: -7,
                w: -7,
                y: -7,
                a: -1,
                '"': -3,
                "'": -3,
                ",": 3,
                ".": 3
            }
        },
        d: {
            d: "97,-249r27,0r2,249r-24,0v-2,-7,0,-17,-3,-22v-6,12,-18,25,-37,25v-29,0,-50,-31,-50,-89v0,-62,25,-91,52,-91v14,-1,24,8,33,21r0,-93xm69,-20v27,5,33,-61,27,-99v-2,-18,-12,-34,-25,-34v-23,0,-31,32,-31,67v0,37,9,66,29,66",
            w: 143,
            k: {
                ",": 5,
                ".": 5
            }
        },
        e: {
            d: "120,-85r-81,0v1,70,31,72,70,59r4,20v-55,24,-101,-8,-101,-79v0,-56,24,-92,59,-92v39,1,51,46,49,92xm39,-105r55,0v0,-35,-13,-50,-26,-50v-18,0,-28,26,-29,50",
            w: 130,
            k: {
                T: 16,
                x: 3,
                z: 3,
                "-": -6,
                '"': 3,
                "'": 3,
                ",": 4,
                ".": 4
            }
        },
        f: {
            d: "25,-174v-7,-48,25,-92,72,-74r-4,22v-31,-13,-44,16,-40,52r32,0r0,21r-32,0r0,153r-28,0r0,-153r-22,0r0,-21r22,0",
            w: 82,
            k: {
                g: 1,
                "ğ": 1,
                c: 1,
                d: 1,
                e: 1,
                o: 1,
                q: 1,
                "ç": 1,
                "ö": 1,
                t: -2,
                ":": -13,
                ";": -13,
                "-": 1,
                ")": -25,
                "]": -25,
                "}": -25,
                '"': -17,
                "'": -17,
                ",": 13,
                ".": 13
            }
        },
        g: {
            d: "125,-22v8,88,-43,105,-102,85r6,-21v32,13,72,12,68,-46v-1,-6,2,-14,-1,-18v-6,14,-18,22,-33,22v-31,0,-51,-35,-51,-86v0,-64,29,-91,55,-91v19,-1,26,14,33,22r2,-19r24,0xm71,-23v27,0,28,-54,26,-91v0,-20,-6,-39,-26,-39v-20,0,-31,27,-31,66v0,44,14,64,31,64",
            w: 143,
            k: {
                T: 17,
                '"': 3,
                "'": 3,
                ",": 8,
                ".": 8
            }
        },
        h: {
            d: "75,-153v-47,0,-22,101,-28,153r-28,0r0,-249r28,0r1,94v22,-36,78,-31,78,39r0,116r-27,0r0,-112v0,-21,-5,-41,-24,-41",
            w: 145,
            k: {
                T: 18,
                t: 2,
                v: 3,
                w: 3,
                y: 3
            }
        },
        i: {
            d: "47,0r-28,0r0,-174r28,0r0,174xm33,-237v10,0,16,7,16,18v0,11,-7,18,-17,18v-9,0,-15,-7,-15,-18v0,-11,7,-18,16,-18",
            w: 66
        },
        j: {
            d: "-10,50v27,-9,32,-20,32,-69r0,-155r27,0v-5,73,15,188,-17,229v-9,12,-27,17,-38,17xm36,-237v10,0,16,7,16,18v0,11,-7,18,-17,18v-9,0,-16,-7,-16,-18v0,-11,8,-18,17,-18",
            w: 67,
            k: {
                ",": 5,
                ".": 5
            }
        },
        k: {
            d: "46,-249r1,156v14,-29,29,-54,44,-81r30,0r-46,73r50,101r-31,0r-37,-82v-17,14,-10,52,-11,82r-27,0r0,-249r27,0",
            w: 127,
            k: {
                T: 14,
                g: 5,
                "ğ": 5,
                i: 1,
                m: 1,
                n: 1,
                p: 1,
                r: 1,
                "ı": 1,
                c: 5,
                d: 5,
                e: 5,
                o: 5,
                q: 5,
                "ç": 5,
                "ö": 5,
                u: 1,
                "ü": 1,
                a: -2,
                "-": 5,
                ",": -3,
                ".": -3
            }
        },
        l: {
            d: "19,0r0,-249r28,0r0,249r-28,0",
            w: 68,
            k: {
                ",": 5,
                ".": 5
            }
        },
        m: {
            d: "74,-154v-45,0,-22,102,-27,154r-28,0r-1,-174r24,0r2,21v15,-30,65,-33,77,3v21,-42,83,-38,83,35r0,115r-28,0r0,-109v0,-23,-3,-45,-24,-45v-46,0,-21,102,-27,154r-27,0r0,-114v0,-19,-3,-40,-24,-40",
            w: 223,
            k: {
                T: 18,
                t: 2,
                v: 3,
                w: 3,
                y: 3
            }
        },
        n: {
            d: "75,-153v-46,0,-23,101,-28,153r-28,0r-1,-174r24,0r2,21v23,-39,82,-32,82,36r0,117r-27,0r0,-112v0,-21,-4,-41,-24,-41",
            w: 145,
            k: {
                T: 18,
                t: 2,
                v: 3,
                w: 3,
                y: 3
            }
        },
        o: {
            d: "70,-177v31,0,56,30,56,90v-1,126,-114,118,-114,0v0,-63,28,-90,58,-90xm69,-155v-25,0,-29,41,-29,68v0,29,5,68,30,68v24,0,29,-41,29,-68v0,-27,-6,-68,-30,-68",
            w: 137,
            k: {
                T: 16,
                v: 3,
                w: 3,
                y: 3,
                x: 5,
                z: 3,
                "-": -5,
                '"': 7,
                "'": 7,
                ",": 12,
                ".": 12
            }
        },
        p: {
            d: "19,69r-1,-243r24,0r2,22v10,-18,23,-25,37,-25v31,0,51,33,51,90v0,65,-27,90,-54,90v-18,1,-25,-12,-32,-20r0,86r-27,0xm73,-153v-31,0,-27,52,-27,89v-1,23,16,49,27,44v23,0,31,-32,31,-67v0,-36,-9,-66,-31,-66",
            w: 143,
            k: {
                T: 16,
                v: 3,
                w: 3,
                y: 3,
                x: 5,
                z: 3,
                "-": -5,
                '"': 7,
                "'": 7,
                ",": 12,
                ".": 12
            }
        },
        q: {
            d: "97,69r-1,-87v-6,12,-18,21,-34,21v-28,0,-50,-31,-50,-88v0,-62,26,-92,55,-92v12,0,23,7,33,23r1,-20r25,0r-2,243r-27,0xm70,-20v26,0,32,-63,26,-101v-2,-18,-12,-32,-25,-32v-23,0,-31,31,-31,67v0,37,8,66,30,66",
            w: 143,
            k: {
                T: 14,
                ",": 3,
                ".": 3
            }
        },
        r: {
            d: "87,-148v-60,-2,-35,91,-40,148r-28,0r-1,-174r24,0v2,8,0,20,3,27v7,-18,22,-33,42,-29r0,28",
            w: 89,
            k: {
                T: 10,
                f: -9,
                g: 5,
                "ğ": 5,
                c: 5,
                d: 5,
                e: 5,
                o: 5,
                q: 5,
                "ç": 5,
                "ö": 5,
                t: -9,
                u: 1,
                "ü": 1,
                v: -9,
                w: -9,
                y: -9,
                x: -8,
                z: -3,
                a: 2,
                ",": 19,
                ".": 19
            }
        },
        s: {
            d: "37,-132v2,30,65,44,58,82v2,49,-53,64,-87,42r7,-21v15,13,53,13,53,-16v0,-32,-66,-50,-57,-84v-3,-41,50,-60,79,-39r-7,21v-15,-14,-48,-9,-46,15",
            w: 104,
            k: {
                T: 11,
                ",": 5,
                ".": 5
            }
        },
        t: {
            d: "84,-1v-36,9,-58,-3,-58,-51r0,-101r-22,0r0,-21r22,0r0,-30r27,-11r0,41r34,0r0,21r-34,0r0,103v1,29,11,31,29,28",
            w: 91,
            k: {
                g: 3,
                "ğ": 3,
                c: 3,
                d: 3,
                e: 3,
                o: 3,
                q: 3,
                "ç": 3,
                "ö": 3,
                v: -4,
                w: -4,
                y: -4,
                ",": 5,
                ".": 5
            }
        },
        u: {
            d: "70,-21v22,0,27,-18,26,-38r0,-115r28,0r1,174r-24,0v-1,-6,0,-14,-3,-18v-5,9,-18,21,-37,21v-62,0,-37,-114,-42,-177r27,0r0,106v0,27,5,47,24,47",
            w: 143,
            k: {
                T: 14,
                ",": 3,
                ".": 3
            }
        },
        v: {
            d: "5,-174r28,0r31,140v7,-50,19,-93,29,-140r27,0r-45,174r-25,0",
            w: 124,
            k: {
                T: 11,
                g: 4,
                "ğ": 4,
                c: 5,
                d: 5,
                e: 5,
                o: 5,
                q: 5,
                "ç": 5,
                "ö": 5,
                v: -7,
                w: -7,
                y: -7,
                a: 3,
                ":": 1,
                ";": 1,
                "-": 1,
                ",": 15,
                ".": 15
            }
        },
        w: {
            d: "5,-174r28,0r28,141r31,-141r21,0r32,141v5,-47,17,-96,26,-141r28,0r-44,174r-23,0r-30,-137v-8,50,-21,90,-31,137r-24,0",
            w: 203,
            k: {
                T: 11,
                g: 4,
                "ğ": 4,
                c: 5,
                d: 5,
                e: 5,
                o: 5,
                q: 5,
                "ç": 5,
                "ö": 5,
                v: -7,
                w: -7,
                y: -7,
                a: 3,
                ":": 1,
                ";": 1,
                "-": 1,
                ",": 15,
                ".": 15
            }
        },
        x: {
            d: "5,-174r30,0r28,67r27,-67r29,0r-42,86r41,88r-29,0r-29,-69r-28,69r-28,0r41,-87",
            w: 122,
            k: {
                T: 11,
                g: 1,
                "ğ": 1,
                c: 5,
                d: 5,
                e: 5,
                o: 5,
                q: 5,
                "ç": 5,
                "ö": 5,
                s: 1,
                "ş": 1,
                t: -5,
                v: -7,
                w: -7,
                y: -7,
                "-": 1
            }
        },
        y: {
            d: "7,53v19,-12,49,-38,41,-67r-44,-160r29,0r32,136r28,-136r27,0v-27,88,-32,211,-103,250",
            w: 124,
            k: {
                T: 11,
                g: 4,
                "ğ": 4,
                c: 5,
                d: 5,
                e: 5,
                o: 5,
                q: 5,
                "ç": 5,
                "ö": 5,
                v: -7,
                w: -7,
                y: -7,
                a: 3,
                ":": 1,
                ";": 1,
                "-": 1,
                ",": 15,
                ".": 15
            }
        },
        z: {
            d: "6,0r0,-17r69,-134r-62,0r0,-23r92,0r0,18r-68,133r68,0r0,23r-99,0",
            w: 112,
            k: {
                T: 9,
                c: 4,
                d: 4,
                e: 4,
                o: 4,
                q: 4,
                "ç": 4,
                "ö": 4,
                v: -5,
                w: -5,
                y: -5
            }
        },
        "{": {
            d: "31,-6v-2,-34,22,-85,-21,-89r0,-18v44,-3,19,-54,21,-88v2,-33,21,-47,49,-46r0,19v-58,0,7,113,-47,125v33,4,23,61,21,97v0,17,6,28,26,28r0,18v-28,1,-47,-10,-49,-46",
            w: 91,
            k: {
                T: -4,
                J: -3,
                C: 7,
                G: 7,
                O: 7,
                Q: 7,
                "Ç": 7,
                "Ö": 7,
                "Ğ": 7,
                V: -8,
                W: -8,
                X: -1,
                Y: -10,
                A: 6,
                j: -11,
                c: 4,
                d: 4,
                e: 4,
                o: 4,
                q: 4,
                "ç": 4,
                "ö": 4
            }
        },
        "|": {
            d: "25,-270r23,0r0,360r-23,0r0,-360",
            w: 72
        },
        "}": {
            d: "60,-201v2,34,-23,85,21,88r0,18v-44,4,-19,55,-21,89v-2,36,-21,47,-49,46r0,-18v60,-1,-8,-115,47,-126v-33,-5,-23,-60,-21,-97v0,-17,-7,-27,-26,-27r0,-19v29,-1,47,13,49,46",
            w: 91
        },
        "~": {
            d: "152,-78v-22,5,-68,-30,-88,-30v-12,0,-18,9,-18,28r-20,0v-1,-31,16,-48,40,-48v22,0,67,31,86,30v13,0,17,-12,17,-28r19,0v2,32,-12,48,-36,48",
            w: 214
        },
        "'": {
            d: "18,-246r26,0r-5,84r-16,0",
            w: 61,
            k: {
                T: -2,
                J: 12,
                C: 1,
                G: 1,
                O: 1,
                Q: 1,
                "Ç": 1,
                "Ö": 1,
                "Ğ": 1,
                V: -2,
                W: -2,
                X: -2,
                Y: -4,
                A: 22,
                f: -5,
                g: 8,
                "ğ": 8,
                c: 8,
                d: 8,
                e: 8,
                o: 8,
                q: 8,
                "ç": 8,
                "ö": 8,
                s: 1,
                "ş": 1,
                t: -5,
                x: -1,
                ",": 33,
                ".": 33
            }
        },
        "`": {
            d: "14,-246r28,0r28,49r-18,0",
            w: 108
        },
        "ı": {
            d: "47,0r-28,0r0,-174r28,0r0,174",
            w: 66
        },
        "Ç": {
            d: "123,-28r4,24v-6,3,-17,6,-29,6r-8,16v12,2,23,11,23,25v0,31,-36,35,-56,22r5,-15v10,6,31,10,31,-6v0,-10,-11,-14,-24,-15r12,-28v-41,-6,-69,-42,-69,-119v0,-111,52,-140,115,-121r-6,24v-47,-16,-80,8,-80,94v0,88,31,108,82,93",
            w: 142,
            k: {
                T: -3,
                J: -5,
                C: 6,
                G: 6,
                O: 6,
                Q: 6,
                "Ç": 6,
                "Ö": 6,
                "Ğ": 6,
                V: -3,
                W: -3,
                X: -4,
                Y: -6,
                A: -2,
                b: 1,
                h: 1,
                k: 1,
                l: 1,
                i: 1,
                m: 1,
                n: 1,
                p: 1,
                r: 1,
                "ı": 1,
                c: 6,
                d: 6,
                e: 6,
                o: 6,
                q: 6,
                "ç": 6,
                "ö": 6,
                u: 6,
                "ü": 6,
                v: 13,
                w: 13,
                y: 13,
                z: -5,
                a: 4,
                ")": -2,
                "]": -2,
                "}": -2,
                '"': -3,
                "'": -3
            }
        },
        "Ö": {
            d: "83,-246v41,0,66,46,66,123v0,91,-31,126,-70,126v-40,0,-67,-43,-67,-125v0,-80,29,-124,71,-124xm81,-221v-56,1,-56,199,0,199v55,-3,54,-195,0,-199xm51,-261v-9,0,-15,-7,-15,-17v0,-9,6,-16,15,-16v9,0,16,7,16,16v0,10,-6,17,-16,17xm109,-261v-9,0,-15,-7,-15,-17v0,-9,6,-16,15,-16v9,0,16,7,16,16v0,10,-6,17,-16,17",
            w: 160,
            k: {
                T: 9,
                J: 3,
                V: 3,
                W: 3,
                X: 4,
                Y: 7,
                A: 7,
                f: -3,
                g: -3,
                "ğ": -3,
                b: -1,
                h: -1,
                k: -1,
                l: -1,
                i: -2,
                m: -2,
                n: -2,
                p: -2,
                r: -2,
                "ı": -2,
                c: -3,
                d: -3,
                e: -3,
                o: -3,
                q: -3,
                "ç": -3,
                "ö": -3,
                t: -3,
                u: -3,
                "ü": -3,
                v: -3,
                w: -3,
                y: -3,
                x: 1,
                z: 3,
                "-": -2,
                ")": 2,
                "]": 2,
                "}": 2,
                '"': 1,
                "'": 1,
                ",": 15,
                ".": 15
            }
        },
        "Ü": {
            d: "21,-243r28,0r0,163v0,44,15,58,33,58v21,0,34,-16,34,-58r0,-163r27,0r0,159v0,63,-26,87,-62,87v-36,0,-60,-24,-60,-84r0,-162xm53,-261v-9,0,-16,-7,-16,-17v0,-9,7,-16,16,-16v9,0,15,7,15,16v0,10,-5,17,-15,17xm111,-261v-9,0,-15,-7,-15,-17v0,-9,6,-16,15,-16v9,0,16,7,16,16v0,10,-6,17,-16,17",
            w: 164,
            k: {
                M: 2,
                Y: 2,
                A: 4,
                g: 1,
                "ğ": 1,
                c: 1,
                d: 1,
                e: 1,
                o: 1,
                q: 1,
                "ç": 1,
                "ö": 1,
                s: 3,
                "ş": 3,
                t: 1,
                v: 3,
                w: 3,
                y: 3,
                x: 4,
                z: 5,
                a: 2,
                ",": 10,
                ".": 10
            }
        },
        "ç": {
            d: "101,-26r4,21v-6,4,-17,7,-28,7r-8,16v12,2,24,12,24,26v0,31,-37,33,-57,22r6,-16v9,7,30,12,31,-5v0,-10,-11,-15,-25,-16r13,-29v-30,-7,-49,-36,-49,-86v0,-59,39,-108,93,-85r-5,22v-37,-17,-60,20,-60,63v0,50,24,76,61,60",
            w: 108,
            k: {
                T: 10,
                f: -4,
                c: 2,
                d: 2,
                e: 2,
                o: 2,
                q: 2,
                "ç": 2,
                "ö": 2,
                t: -5,
                u: -1,
                "ü": -1,
                v: -7,
                w: -7,
                y: -7,
                a: -1,
                '"': -3,
                "'": -3,
                ",": 3,
                ".": 3
            }
        },
        "ö": {
            d: "70,-177v31,0,56,30,56,90v-1,126,-114,118,-114,0v0,-63,28,-90,58,-90xm69,-155v-25,0,-29,41,-29,68v0,29,5,68,30,68v24,0,29,-41,29,-68v0,-27,-6,-68,-30,-68xm41,-204v-9,0,-16,-7,-16,-16v0,-9,7,-16,16,-16v8,0,16,7,16,16v0,9,-7,16,-16,16xm97,-204v-9,0,-16,-7,-16,-16v0,-9,8,-16,17,-16v8,0,15,7,15,16v0,9,-7,16,-16,16",
            w: 137,
            k: {
                T: 16,
                v: 3,
                w: 3,
                y: 3,
                x: 5,
                z: 3,
                "-": -5,
                '"': 7,
                "'": 7,
                ",": 12,
                ".": 12
            }
        },
        "ü": {
            d: "70,-21v22,0,27,-18,26,-38r0,-115r28,0r1,174r-24,0v-1,-6,0,-14,-3,-18v-5,9,-18,21,-37,21v-62,0,-37,-114,-42,-177r27,0r0,106v0,27,5,47,24,47xm45,-204v-9,0,-16,-7,-16,-16v0,-9,7,-16,16,-16v8,0,15,7,15,16v0,9,-6,16,-15,16xm101,-204v-9,0,-16,-7,-16,-16v0,-9,7,-16,16,-16v8,0,15,7,15,16v0,9,-6,16,-15,16",
            w: 143,
            k: {
                T: 14,
                ",": 3,
                ".": 3
            }
        },
        "Ğ": {
            d: "141,-6v-76,28,-134,-23,-129,-112v6,-111,57,-141,125,-120r-6,24v-51,-18,-91,4,-91,93v0,84,28,105,75,97r0,-80r-31,0r0,-23r57,0r0,121xm53,-291r16,0v2,21,42,22,44,0r17,0v0,21,-15,35,-39,35v-24,0,-38,-13,-38,-35",
            w: 156,
            k: {
                b: -1,
                h: -1,
                k: -1,
                l: -1,
                i: -1,
                m: -1,
                n: -1,
                p: -1,
                r: -1,
                "ı": -1,
                c: -1,
                d: -1,
                e: -1,
                o: -1,
                q: -1,
                "ç": -1,
                "ö": -1,
                u: -1,
                "ü": -1,
                v: 3,
                w: 3,
                y: 3,
                a: -2
            }
        },
        "İ": {
            d: "22,-243r27,0r0,243r-27,0r0,-243xm36,-262v-9,0,-16,-7,-16,-16v0,-9,7,-16,16,-16v9,0,16,7,16,16v0,9,-7,16,-16,16",
            w: 71,
            k: {
                j: 1,
                c: 1,
                d: 1,
                e: 1,
                o: 1,
                q: 1,
                "ç": 1,
                "ö": 1,
                s: 1,
                "ş": 1,
                v: 1,
                w: 1,
                y: 1,
                x: 1,
                z: -1
            }
        },
        "Ş": {
            d: "91,-63v-3,-53,-77,-60,-77,-118v0,-51,57,-79,98,-55r-7,24v-20,-17,-71,-4,-64,26v0,24,12,35,36,50v57,35,54,117,-5,135r-10,19v12,2,24,12,24,26v0,31,-37,33,-57,22r6,-16v9,7,29,12,31,-5v0,-10,-12,-15,-25,-16r13,-27v-18,0,-37,-5,-44,-10r6,-24v27,19,77,10,75,-31",
            w: 129,
            k: {
                i: -1,
                m: -1,
                n: -1,
                p: -1,
                r: -1,
                "ı": -1,
                c: -3,
                d: -3,
                e: -3,
                o: -3,
                q: -3,
                "ç": -3,
                "ö": -3,
                t: 1,
                u: -2,
                "ü": -2,
                v: 4,
                w: 4,
                y: 4,
                a: -3,
                "-": -2
            }
        },
        "ğ": {
            d: "125,-22v8,88,-43,105,-102,85r6,-21v32,13,72,12,68,-46v-1,-6,2,-14,-1,-18v-6,14,-18,22,-33,22v-31,0,-51,-35,-51,-86v0,-64,29,-91,55,-91v19,-1,26,14,33,22r2,-19r24,0xm71,-23v27,0,28,-54,26,-91v0,-20,-6,-39,-26,-39v-20,0,-31,27,-31,66v0,44,14,64,31,64xm34,-241r15,0v1,13,10,22,22,22v14,0,23,-9,23,-22r16,0v1,23,-15,41,-39,41v-24,0,-38,-18,-37,-41",
            w: 143,
            k: {
                T: 17,
                '"': 3,
                "'": 3,
                ",": 8,
                ".": 8
            }
        },
        "ş": {
            d: "37,-132v2,30,58,43,58,82v0,27,-16,44,-35,50r-9,18v12,2,24,12,24,26v0,31,-37,33,-57,22r5,-16v9,7,30,11,31,-5v0,-10,-10,-15,-24,-16v3,-9,11,-20,11,-27v-10,0,-24,-3,-33,-10r7,-21v15,13,53,13,53,-16v0,-32,-66,-50,-57,-84v-3,-41,50,-60,79,-39r-7,21v-15,-14,-48,-9,-46,15",
            w: 104,
            k: {
                T: 11,
                ",": 5,
                ".": 5
            }
        },
        " ": {
            w: 56,
            k: {
                T: 13,
                V: 13,
                W: 13,
                Y: 13
            }
        }
    }
}), (function ($) {
    var m = (jQuery.browser.msie && parseInt(jQuery.browser.version, 10) < 7 && parseInt(jQuery.browser.version, 10) > 4);
    if ($.proxy === undefined) {
        $.extend({
            proxy: function (a, b) {
                if (a) {
                    proxy = function () {
                        return a.apply(b || this, arguments)
                    }
                };
                return proxy
            }
        })
    };
    $.extend(jQuery.easing, {
        easeOutBack: function (x, t, b, c, d, s) {
            if (s == undefined) s = 1.70158;
            return c * ((t = t / d - 1) * t * ((s + 1) * t + s) + 1) + b
        }
    });
    $.extend($.expr[':'], {
        value: function (a) {
            return $(a).val()
        }
    });
    $.extend({
        MsgBoxObject: {
            defaults: {
                name: 'jquery-msgbox',
                zIndex: 10000,
                width: 420,
                height: 'auto',
                background: '#FFFFFF',
                modal: true,
                overlay: {
                    'background-color': '#000000',
                    'opacity': 0.5
                },
                showDuration: 200,
                closeDuration: 100,
                moveDuration: 500,
                shake: {
                    'distance': 10,
                    'duration': 100,
                    'transition': 'easeOutBack',
                    'loops': 2
                },
                form: {
                    'active': false,
                    'action': '#',
                    'method': 'post'
                },
                emergefrom: 'top'
            },
            options: {},
            esqueleto: {
                msgbox: [],
                wrapper: [],
                form: [],
                buttons: [],
                inputs: []
            },
            visible: false,
            i: 0,
            animation: false,
            config: function (a) {
                this.options = $.extend(true, this.options, a);
                this.overlay.element.css(this.options.overlay);
                this.overlay.options.hideOnClick = !this.options.modal;
                this.esqueleto.msgbox.css({
                    'width': this.options.width,
                    'height': this.options.height,
                    'background-color': this.options.background
                });
                this.moveBox()
            },
            overlay: {
                create: function (b) {
                    this.options = b;
                    this.element = $('<div id="' + new Date().getTime() + '"></div>');
                    this.element.css($.extend({}, {
                        'position': 'fixed',
                        'top': 0,
                        'left': 0,
                        'opacity': 0,
                        'display': 'none',
                        'z-index': this.options.zIndex
                    }, this.options.style));
                    this.element.click($.proxy(function (a) {
                        if (this.options.hideOnClick) {
                            if ($.isFunction(this.options.callback)) {
                                this.options.callback()
                            } else {
                                this.hide()
                            }
                        }
                        a.preventDefault()
                    }, this));
                    this.hidden = true;
                    this.inject();
                    return this
                },
                inject: function () {
                    this.target = $(document.body);
                    this.target.append(this.element);
                    if (m) {
                        this.element.css({
                            'position': 'absolute'
                        });
                        var a = parseInt(this.element.css('zIndex'));
                        if (!a) {
                            a = 1;
                            var b = this.element.css('position');
                            if (b == 'static' || !b) {
                                this.element.css({
                                    'position': 'relative'
                                })
                            }
                            this.element.css({
                                'zIndex': a
                            })
                        }
                        a = ( !! (this.options.zIndex || this.options.zIndex === 0) && a > this.options.zIndex) ? this.options.zIndex : a - 1;
                        if (a < 0) {
                            a = 1
                        }
                        this.shim = $('<iframe id="IF_' + new Date().getTime() + '" scrolling="no" frameborder=0 src=""></div>');
                        this.shim.css({
                            zIndex: a,
                            position: 'absolute',
                            top: 0,
                            left: 0,
                            border: 'none',
                            width: 0,
                            height: 0,
                            opacity: 0
                        });
                        this.shim.insertAfter(this.element);
                        $('html, body').css({
                            'height': '100%',
                            'width': '100%',
                            'margin-left': 0,
                            'margin-right': 0
                        })
                    }
                },
                resize: function (x, y) {
                    this.element.css({
                        'height': 0,
                        'width': 0
                    });
                    if (this.shim) this.shim.css({
                        'height': 0,
                        'width': 0
                    });
                    var a = {
                        x: $(document).width(),
                        y: $(document).height()
                    };
                    this.element.css({
                        'width': '100%',
                        'height': y ? y : a.y
                    });
                    if (this.shim) {
                        this.shim.css({
                            'height': 0,
                            'width': 0
                        });
                        this.shim.css({
                            'position': 'absolute',
                            'left': 0,
                            'top': 0,
                            'width': this.element.width(),
                            'height': y ? y : a.y
                        })
                    }
                    return this
                },
                show: function () {
                    if (!this.hidden) return this;
                    if (this.transition) this.transition.stop();
                    this.target.bind('resize', $.proxy(this.resize, this));
                    this.resize();
                    if (this.shim) this.shim.css({
                        'display': 'block'
                    });
                    this.hidden = false;
                    this.transition = this.element.fadeIn(this.options.showDuration, $.proxy(function () {
                        this.element.trigger('show')
                    }, this));
                    return this
                },
                hide: function () {
                    if (this.hidden) return this;
                    if (this.transition) this.transition.stop();
                    this.target.unbind('resize');
                    if (this.shim) this.shim.css({
                        'display': 'none'
                    });
                    this.hidden = true;
                    this.transition = this.element.fadeOut(this.options.closeDuration, $.proxy(function () {
                        this.element.trigger('hide');
                        this.element.css({
                            'height': 0,
                            'width': 0
                        })
                    }, this));
                    return this
                }
            },
            create: function () {
                this.options = $.extend(true, this.defaults, this.options);
                this.overlay.create({
                    style: this.options.overlay,
                    hideOnClick: !this.options.modal,
                    zIndex: this.options.zIndex - 1,
                    showDuration: this.options.showDuration,
                    closeDuration: this.options.closeDuration
                });
                this.esqueleto.msgbox = $('<div class="' + this.options.name + '"></div>');
                this.esqueleto.msgbox.css({
                    'display': 'none',
                    'position': 'absolute',
                    'top': 0,
                    'left': 0,
                    'width': this.options.width,
                    'height': this.options.height,
                    'z-index': this.options.zIndex,
                    'word-wrap': 'break-word',
                    '-moz-box-shadow': '0 0 15px rgba(0, 0, 0, 0.5)',
                    '-webkit-box-shadow': '0 0 15px rgba(0, 0, 0, 0.5)',
                    'box-shadow': '0 0 15px rgba(0, 0, 0, 0.5)',
                    '-moz-border-radius': '6px',
                    '-webkit-border-radius': '6px',
                    'border-radius': '6px',
                    'background-color': this.options.background
                });
                this.esqueleto.wrapper = $('<div class="' + this.options.name + '-wrapper"></div>');
                this.esqueleto.msgbox.append(this.esqueleto.wrapper);
                this.esqueleto.form = $('<form action="' + this.options.formaction + '" method="post"></form>');
                this.esqueleto.wrapper.append(this.esqueleto.form);
                this.esqueleto.wrapper.css({
                    height: (m ? 80 : 'auto'),
                    'min-height': 80,
                    'zoom': 1
                });
                $('body').append(this.esqueleto.msgbox);
                this.addevents();
                return this.esqueleto.msgbox
            },
            addevents: function () {
                $(window).bind('resize', $.proxy(function () {
                    if (this.visible) {
                        this.overlay.resize();
                        this.moveBox()
                    }
                }, this));
                $(window).bind('scroll', $.proxy(function () {
                    if (this.visible) {
                        this.moveBox()
                    }
                }, this));
                this.esqueleto.msgbox.bind('keydown', $.proxy(function (a) {
                    if (a.keyCode == 27) {
                        this.close(false)
                    }
                }, this));
                this.esqueleto.form.bind('submit', $.proxy(function (a) {
                    $('input[type=submit]:first, button[type=submit]:first, button:first', this.esqueleto.form).trigger('click');
                    if (!options.form.active) {
                        a.preventDefault()
                    }
                }, this));
                this.overlay.element.bind('show', $.proxy(function () {
                    $(this).triggerHandler('show')
                }, this));
                this.overlay.element.bind('hide', $.proxy(function () {
                    $(this).triggerHandler('close')
                }, this))
            },
            show: function (g, h, j) {
                var k = ['alert', 'info', 'error', 'prompt', 'confirm'];
                this.esqueleto.msgbox.queue(this.options.name, $.proxy(function (c) {
                    h = $.extend(true, {
                        type: 'alert',
                        form: {
                            'active': false
                        }
                    }, h || {});
                    if (typeof h.buttons === "undefined") {
                        if (h.type == 'confirm' || h.type == 'prompt') {
                            var d = [{
                                type: 'submit',
                                value: 'Accept'
                            }, {
                                type: 'cancel',
                                value: 'Cancel'
                            }]
                        } else {
                            var d = [{
                                type: 'submit',
                                value: 'Accept'
                            }]
                        }
                    } else {
                        var d = h.buttons
                    };
                    if (typeof h.inputs === "undefined" && h.type == 'prompt') {
                        var f = [{
                            type: 'text',
                            name: 'prompt',
                            value: ''
                        }]
                    } else {
                        var f = h.inputs
                    };
                    this.callback = $.isFunction(j) ? j : function (e) {};
                    if (typeof f !== "undefined") {
                        this.esqueleto.inputs = $('<div class="' + this.options.name + '-inputs"></div>');
                        this.esqueleto.form.append(this.esqueleto.inputs);
                        $.each(f, $.proxy(function (i, a) {
                            if (a.type == 'checkbox') {
                                iLabel = a.label ? '<label class="' + this.options.name + '-label">' : '';
                                fLabel = a.label ? a.label + '</label>' : '';
                                a.value = a.value === undefined ? '1' : a.value;
                                iName = a.name === undefined ? this.options.name + '-label-' + i : a.name;
                                this.esqueleto.inputs.append($(iLabel + '<input type="' + a.type + '" style="display:inline; width:auto;" name="' + iName + '" value="' + a.value + '" autocomplete="off"/> ' + fLabel))
                            } else {
                                iLabel = a.label ? '<label class="' + this.options.name + '-label">' + a.label : '';
                                fLabel = a.label ? '</label>' : '';
                                a.value = a.value === undefined ? '' : a.value;
                                iRequired = a.required === undefined || a.required == false ? '' : 'required="true"';
                                iName = a.name === undefined ? this.options.name + '-label-' + i : a.name;
                                this.esqueleto.inputs.append($(iLabel + '<input type="' + a.type + '" name="' + iName + '" value="' + a.value + '" autocomplete="off" ' + iRequired + '/>' + fLabel))
                            }
                        }, this))
                    }
                    this.esqueleto.buttons = $('<div class="' + this.options.name + '-buttons"></div>');
                    this.esqueleto.form.append(this.esqueleto.buttons);
                    if (h.form.active) {
                        this.esqueleto.form.attr('action', h.form.action === undefined ? '#' : h.form.action);
                        this.esqueleto.form.attr('method', h.form.method === undefined ? 'post' : h.form.method);
                        this.options.form.active = true
                    } else {
                        this.esqueleto.form.attr('action', '#');
                        this.esqueleto.form.attr('method', 'post');
                        this.options.form.active = false
                    }
                    if (h.type != 'prompt') {
                        $.each(d, $.proxy(function (i, a) {
                            if (a.type == 'submit') {
                                this.esqueleto.buttons.append($('<button type="submit">' + a.value + '</button>').bind('click', $.proxy(function (e) {
                                    this.close(a.value);
                                    e.preventDefault()
                                }, this)))
                            } else if (a.type == 'cancel') {
                                this.esqueleto.buttons.append($('<button type="button">' + a.value + '</button>').bind('click', $.proxy(function (e) {
                                    this.close(false);
                                    e.preventDefault()
                                }, this)))
                            }
                        }, this))
                    } else if (h.type == 'prompt') {
                        $.each(d, $.proxy(function (i, a) {
                            if (a.type == 'submit') {
                                this.esqueleto.buttons.append($('<button type="submit">' + a.value + '</button>').bind('click', $.proxy(function (e) {
                                    if ($('input[required="true"]:not(:value)').length > 0) {
                                        $('input[required="true"]:not(:value):first').focus();
                                        this.shake()
                                    } else if (this.options.form.active) {
                                        return true
                                    } else {
                                        this.close(this.toArguments($('input', this.esqueleto.inputs)))
                                    }
                                    e.preventDefault()
                                }, this)))
                            } else if (a.type == 'cancel') {
                                this.esqueleto.buttons.append($('<button type="button">' + a.value + '</button>').bind('click', $.proxy(function (e) {
                                    this.close(false);
                                    e.preventDefault()
                                }, this)))
                            }
                        }, this))
                    };
                    this.esqueleto.form.prepend(g);
                    $.each(k, $.proxy(function (i, e) {
                        this.esqueleto.wrapper.removeClass(this.options.name + '-' + e)
                    }, this));
                    this.esqueleto.wrapper.addClass(this.options.name + '-' + h.type);
                    this.moveBox();
                    this.visible = true;
                    this.overlay.show();
                    this.esqueleto.msgbox.css({
                        display: 'block',
                        left: (($(document).width() - this.options.width) / 2)
                    });
                    this.moveBox();
                    setTimeout($.proxy(function () {
                        var b = $('input, button', this.esqueleto.msgbox);
                        if (b.length) {
                            b.get(0).focus()
                        }
                    }, this), this.options.moveDuration)
                }, this));
                this.i++;
                if (this.i == 1) {
                    this.esqueleto.msgbox.dequeue(this.options.name)
                }
            },
            toArguments: function (b) {
                return $.map(b, function (a) {
                    return $(a).val()
                })
            },
            moveBox: function () {
                var a = {
                    x: $(window).width(),
                    y: $(window).height()
                };
                var b = {
                    x: $(window).scrollLeft(),
                    y: $(window).scrollTop()
                };
                var c = this.esqueleto.msgbox.outerHeight();
                var y = 0;
                var x = 0;
                y = b.x + ((a.x - this.options.width) / 2);
                if (this.options.emergefrom == "bottom") {
                    x = (b.y + a.y + 80)
                } else {
                    x = (b.y - c) - 80
                }
                if (this.visible) {
                    if (this.animation) {
                        this.animation.stop
                    }
                    this.animation = this.esqueleto.msgbox.animate({
                        left: y,
                        top: b.y + ((a.y - c) / 2)
                    }, {
                        duration: this.options.moveDuration,
                        queue: false,
                        easing: 'easeOutBack'
                    })
                } else {
                    this.esqueleto.msgbox.css({
                        top: x,
                        left: y
                    })
                }
            },
            close: function (a) {
                this.esqueleto.msgbox.css({
                    display: 'none',
                    top: 0
                });
                this.visible = false;
                if ($.isFunction(this.callback)) {
                    this.callback.apply(this, $.makeArray(a))
                }
                setTimeout($.proxy(function () {
                    this.i--;
                    this.esqueleto.msgbox.dequeue(this.options.name)
                }, this), this.options.closeDuration);
                if (this.i == 1) {
                    this.overlay.hide()
                }
                this.moveBox();
                this.esqueleto.form.empty()
            },
            shake: function () {
                var x = this.options.shake.distance;
                var d = this.options.shake.duration;
                var t = this.options.shake.transition;
                var o = this.options.shake.loops;
                var l = this.esqueleto.msgbox.position().left;
                var e = this.esqueleto.msgbox;
                for (i = 0; i < o; i++) {
                    e.animate({
                        left: l + x
                    }, d, t);
                    e.animate({
                        left: l - x
                    }, d, t)
                };
                e.animate({
                    left: l + x
                }, d, t);
                e.animate({
                    left: l
                }, d, t)
            }
        },
        msgbox: function (a, b, c) {
            if (typeof a == "object") {
                $.MsgBoxObject.config(a)
            } else {
                return $.MsgBoxObject.show(a, b, c)
            }
        }
    });
    $(function () {
        if (parseFloat($.fn.jquery) > 1.2) {
            $.MsgBoxObject.create()
        } else {
            throw "The jQuery version that was loaded is too old. MsgBox requires jQuery 1.3+";
        }
    })
})(jQuery),
function (n) {
    function i(i) {
        n.fn.cycle.debug && t(i)
    }
    function t() {
        window.console && window.console.log && window.console.log("[cycle] " + Array.prototype.join.call(arguments, " "))
    }
    function h(i, u, f) {
        function s(i, u, f) {
            if (!i && u === !0) {
                var o = n(f).data("cycle.opts");
                if (!o) return t("options not found, can not resume"), !1;
                f.cycleTimeout && (clearTimeout(f.cycleTimeout), f.cycleTimeout = 0), r(o.elements, o, 1, !e.rev && !e.backwards)
            }
        }
        var e, o;
        if (i.cycleStop == undefined && (i.cycleStop = 0), (u === undefined || u === null) && (u = {}), u.constructor == String) {
            switch (u) {
                case "destroy":
                case "stop":
                    return (e = n(i).data("cycle.opts"), !e) ? !1 : (i.cycleStop++, i.cycleTimeout && clearTimeout(i.cycleTimeout), i.cycleTimeout = 0, n(i).removeData("cycle.opts"), u == "destroy" && y(e), !1);
                case "toggle":
                    return i.cyclePause = i.cyclePause === 1 ? 0 : 1, s(i.cyclePause, f, i), !1;
                case "pause":
                    return i.cyclePause = 1, !1;
                case "resume":
                    return i.cyclePause = 0, s(!1, f, i), !1;
                case "prev":
                case "next":
                    return (e = n(i).data("cycle.opts"), !e) ? (t('options not found, "prev/next" ignored'), !1) : (n.fn.cycle[u](e), !1);
                default:
                    u = {
                        fx: u
                    }
            }
            return u
        }
        return u.constructor == Number ? (o = u, u = n(i).data("cycle.opts"), !u) ? (t("options not found, can not advance slide"), !1) : o < 0 || o >= u.elements.length ? (t("invalid slide index: " + o), !1) : (u.nextSlide = o, i.cycleTimeout && (clearTimeout(i.cycleTimeout), i.cycleTimeout = 0), typeof f == "string" && (u.oneTimeFx = f), r(u.elements, u, 1, o >= u.currSlide), !1) : u
    }
    function o(t, i) {
        if (!n.support.opacity && i.cleartype && t.style.filter) try {
            t.style.removeAttribute("filter")
        } catch (r) {}
    }
    function y(t) {
        t.next && n(t.next).unbind(t.prevNextEvent), t.prev && n(t.prev).unbind(t.prevNextEvent), (t.pager || t.pagerAnchorBuilder) && n.each(t.pagerAnchors || [], function () {
            this.unbind().remove()
        }), t.pagerAnchors = null, t.destroy && t.destroy(t)
    }
    function c(i, e, s, h, c) {
        var y = n.extend({}, n.fn.cycle.defaults, h || {}, n.metadata ? i.metadata() : n.meta ? i.data() : {}),
            st, it, p, ot, d, k, ut, et, ht, ft, b;
        if (y.autostop && (y.countdown = y.autostopCount || s.length), st = i[0], i.data("cycle.opts", y), y.$cont = i, y.stopCount = st.cycleStop, y.elements = s, y.before = y.before ? [y.before] : [], y.after = y.after ? [y.after] : [], y.after.unshift(function () {
            y.busy = 0
        }), !n.support.opacity && y.cleartype && y.after.push(function () {
            o(this, y)
        }), y.continuous && y.after.push(function () {
            r(s, y, 0, !y.rev && !y.backwards)
        }), v(y), n.support.opacity || !y.cleartype || y.cleartypeNoBg || f(e), i.css("position") == "static" && i.css("position", "relative"), y.width && i.width(y.width), y.height && y.height != "auto" && i.height(y.height), y.startingSlide ? y.startingSlide = parseInt(y.startingSlide) : y.backwards && (y.startingSlide = s.length - 1), y.random) {
            for (y.randomMap = [], it = 0; it < s.length; it++) y.randomMap.push(it);
            y.randomMap.sort(function () {
                return Math.random() - .5
            }), y.randomIndex = 1, y.startingSlide = y.randomMap[1]
        } else y.startingSlide >= s.length && (y.startingSlide = 0);
        if (y.currSlide = y.startingSlide || 0, p = y.startingSlide, e.css({
            position: "absolute",
            top: 0,
            left: 0
        }).hide().each(function (t) {
            var i;
            i = y.backwards ? p ? t <= p ? s.length + (t - p) : p - t : s.length - t : p ? t >= p ? s.length - (t - p) : p - t : s.length - t, n(this).css("z-index", i)
        }), n(s[p]).css("opacity", 1).show(), o(s[p], y), y.fit && y.width && e.width(y.width), y.fit && y.height && y.height != "auto" && e.height(y.height), ot = y.containerResize && !i.innerHeight(), ot) {
            for (d = 0, k = 0, ut = 0; ut < s.length; ut++) {
                var g = n(s[ut]),
                    rt = g[0],
                    tt = g.outerWidth(),
                    nt = g.outerHeight();
                tt || (tt = rt.offsetWidth || rt.width || g.attr("width")), nt || (nt = rt.offsetHeight || rt.height || g.attr("height")), d = tt > d ? tt : d, k = nt > k ? nt : k
            }
            d > 0 && k > 0 && i.css({
                width: d + "px",
                height: k + "px"
            })
        }
        if ((y.pause && i.hover(function () {
            this.cyclePause++
        }, function () {
            this.cyclePause--
        }), a(y) === !1) || (et = !1, h.requeueAttempts = h.requeueAttempts || 0, e.each(function () {
            var i = n(this);
            if (this.cycleH = y.fit && y.height ? y.height : i.height() || this.offsetHeight || this.height || i.attr("height") || 0, this.cycleW = y.fit && y.width ? y.width : i.width() || this.offsetWidth || this.width || i.attr("width") || 0, i.is("img")) {
                var f = n.browser.msie && this.cycleW == 28 && this.cycleH == 30 && !this.complete,
                    e = n.browser.mozilla && this.cycleW == 34 && this.cycleH == 19 && !this.complete,
                    r = n.browser.opera && (this.cycleW == 42 && this.cycleH == 19 || this.cycleW == 37 && this.cycleH == 17) && !this.complete,
                    u = this.cycleH == 0 && this.cycleW == 0 && !this.complete;
                if (f || e || r || u) {
                    if (c.s && y.requeueOnImageNotLoaded && ++h.requeueAttempts < 100) return t(h.requeueAttempts, " - img slide not loaded, requeuing slideshow: ", this.src, this.cycleW, this.cycleH), setTimeout(function () {
                        n(c.s, c.c).cycle(h)
        }, y.requeueTimeout), et = !0, !1;
                    t("could not determine size of image: " + this.src, this.cycleW, this.cycleH)
        }
        }
            return !0
        }), et)) return !1;
        if (y.cssBefore = y.cssBefore || {}, y.animIn = y.animIn || {}, y.animOut = y.animOut || {}, e.not(":eq(" + p + ")").css(y.cssBefore), y.cssFirst && n(e[p]).css(y.cssFirst), y.timeout) for (y.timeout = parseInt(y.timeout), y.speed.constructor == String && (y.speed = n.fx.speeds[y.speed] || parseInt(y.speed)), y.sync || (y.speed = y.speed / 2), ht = y.fx == "shuffle" ? 500 : 250; y.timeout - y.speed < ht;) y.timeout += y.speed;
        if (y.easing && (y.easeIn = y.easeOut = y.easing), y.speedIn || (y.speedIn = y.speed), y.speedOut || (y.speedOut = y.speed), y.slideCount = s.length, y.currSlide = y.lastSlide = p, y.random ? (++y.randomIndex == s.length && (y.randomIndex = 0), y.nextSlide = y.randomMap[y.randomIndex]) : y.nextSlide = y.backwards ? y.startingSlide == 0 ? s.length - 1 : y.startingSlide - 1 : y.startingSlide >= s.length - 1 ? 0 : y.startingSlide + 1, !y.multiFx) if (ft = n.fn.cycle.transitions[y.fx], n.isFunction(ft)) ft(i, e, y);
        else if (y.fx != "custom" && !y.multiFx) return t("unknown transition: " + y.fx, "; slideshow terminating"), !1;
        return b = e[p], y.before.length && y.before[0].apply(b, [b, b, y, !0]), y.after.length > 1 && y.after[1].apply(b, [b, b, y, !0]), y.next && n(y.next).bind(y.prevNextEvent, function () {
            return u(y, y.rev ? -1 : 1)
        }), y.prev && n(y.prev).bind(y.prevNextEvent, function () {
            return u(y, y.rev ? 1 : -1)
        }), (y.pager || y.pagerAnchorBuilder) && l(s, y), w(y, s), y
    }
    function v(t) {
        t.original = {
            before: [],
            after: []
        }, t.original.cssBefore = n.extend({}, t.cssBefore), t.original.cssAfter = n.extend({}, t.cssAfter), t.original.animIn = n.extend({}, t.animIn), t.original.animOut = n.extend({}, t.animOut), n.each(t.before, function () {
            t.original.before.push(this)
        }), n.each(t.after, function () {
            t.original.after.push(this)
        })
    }
    function a(r) {
        var u, e, f = n.fn.cycle.transitions,
            o, h, s;
        if (r.fx.indexOf(",") > 0) {
            for (r.multiFx = !0, r.fxs = r.fx.replace(/\s*/g, "").split(","), u = 0; u < r.fxs.length; u++) o = r.fxs[u], e = f[o], e && f.hasOwnProperty(o) && n.isFunction(e) || (t("discarding unknown transition: ", o), r.fxs.splice(u, 1), u--);
            if (!r.fxs.length) return t("No valid transitions named; slideshow terminating."), !1
        } else if (r.fx == "all") {
            r.multiFx = !0, r.fxs = [];
            for (p in f) e = f[p], f.hasOwnProperty(p) && n.isFunction(e) && r.fxs.push(p)
        }
        if (r.multiFx && r.randomizeEffects) {
            for (h = Math.floor(Math.random() * 20) + 30, u = 0; u < h; u++) s = Math.floor(Math.random() * r.fxs.length), r.fxs.push(r.fxs.splice(s, 1)[0]);
            i("randomized fx sequence: ", r.fxs)
        }
        return !0
    }
    function w(t, i) {
        t.addSlide = function (r, u) {
            var e = n(r),
                o = e[0];
            if (t.autostopCount || t.countdown++, i[u ? "unshift" : "push"](o), t.els && t.els[u ? "unshift" : "push"](o), t.slideCount = i.length, e.css("position", "absolute"), e[u ? "prependTo" : "appendTo"](t.$cont), u && (t.currSlide++, t.nextSlide++), n.support.opacity || !t.cleartype || t.cleartypeNoBg || f(e), t.fit && t.width && e.width(t.width), t.fit && t.height && t.height != "auto" && $slides.height(t.height), o.cycleH = t.fit && t.height ? t.height : e.height(), o.cycleW = t.fit && t.width ? t.width : e.width(), e.css(t.cssBefore), (t.pager || t.pagerAnchorBuilder) && n.fn.cycle.createPagerAnchor(i.length - 1, o, n(t.pager), i, t), n.isFunction(t.onAddSlide)) t.onAddSlide(e);
            else e.hide()
        }
    }
    function r(t, u, f, o) {
        var y, v, p, c, a;
        if (f && u.busy && u.manualTrump && (i("manualTrump in go(), stopping active transition"), n(t).stop(!0, !0), u.busy = !1), u.busy) {
            i("transition active, ignoring new tx request");
            return
        }
        var l = u.$cont[0],
            h = t[u.currSlide],
            s = t[u.nextSlide];
        if (l.cycleStop == u.stopCount && (l.cycleTimeout !== 0 || f)) {
            if (!f && !l.cyclePause && !u.bounce && (u.autostop && --u.countdown <= 0 || u.nowrap && !u.random && u.nextSlide < u.currSlide)) {
                u.end && u.end(u);
                return
            }
            y = !1, (f || !l.cyclePause) && u.nextSlide != u.currSlide && (y = !0, v = u.fx, h.cycleH = h.cycleH || n(h).height(), h.cycleW = h.cycleW || n(h).width(), s.cycleH = s.cycleH || n(s).height(), s.cycleW = s.cycleW || n(s).width(), u.multiFx && ((u.lastFx == undefined || ++u.lastFx >= u.fxs.length) && (u.lastFx = 0), v = u.fxs[u.lastFx], u.currFx = v), u.oneTimeFx && (v = u.oneTimeFx, u.oneTimeFx = null), n.fn.cycle.resetState(u, v), u.before.length && n.each(u.before, function (n, t) {
                l.cycleStop == u.stopCount && t.apply(s, [h, s, u, o])
            }), p = function () {
                n.each(u.after, function (n, t) {
                    l.cycleStop == u.stopCount && t.apply(s, [h, s, u, o])
                })
            }, i("tx firing; currSlide: " + u.currSlide + "; nextSlide: " + u.nextSlide), u.busy = 1, u.fxFn ? u.fxFn(h, s, u, p, o, f && u.fastOnEvent) : n.isFunction(n.fn.cycle[u.fx]) ? n.fn.cycle[u.fx](h, s, u, p, o, f && u.fastOnEvent) : n.fn.cycle.custom(h, s, u, p, o, f && u.fastOnEvent)), (y || u.nextSlide == u.currSlide) && (u.lastSlide = u.currSlide, u.random ? (u.currSlide = u.nextSlide, ++u.randomIndex == t.length && (u.randomIndex = 0), u.nextSlide = u.randomMap[u.randomIndex], u.nextSlide == u.currSlide && (u.nextSlide = u.currSlide == u.slideCount - 1 ? 0 : u.currSlide + 1)) : u.backwards ? (c = u.nextSlide - 1 < 0, c && u.bounce ? (u.backwards = !u.backwards, u.nextSlide = 1, u.currSlide = 0) : (u.nextSlide = c ? t.length - 1 : u.nextSlide - 1, u.currSlide = c ? 0 : u.nextSlide + 1)) : (c = u.nextSlide + 1 == t.length, c && u.bounce ? (u.backwards = !u.backwards, u.nextSlide = t.length - 2, u.currSlide = t.length - 1) : (u.nextSlide = c ? 0 : u.nextSlide + 1, u.currSlide = c ? t.length - 1 : u.nextSlide - 1))), y && u.pager && u.updateActivePagerLink(u.pager, u.currSlide, u.activePagerClass), a = 0, u.timeout && !u.continuous ? a = e(t[u.currSlide], t[u.nextSlide], u, o) : u.continuous && l.cyclePause && (a = 10), a > 0 && (l.cycleTimeout = setTimeout(function () {
                r(t, u, 0, !u.rev && !u.backwards)
            }, a))
        }
    }
    function e(n, t, r, u) {
        if (r.timeoutFn) {
            for (var f = r.timeoutFn.call(n, n, t, r, u); f - r.speed < 250;) f += r.speed;
            if (i("calculated timeout: " + f + "; speed: " + r.speed), f !== !1) return f
        }
        return r.timeout
    }
    function u(t, i) {
        var u = t.elements,
            e = t.$cont[0],
            o = e.cycleTimeout,
            f;
        if (o && (clearTimeout(o), e.cycleTimeout = 0), t.random && i < 0) t.randomIndex--, --t.randomIndex == -2 ? t.randomIndex = u.length - 2 : t.randomIndex == -1 && (t.randomIndex = u.length - 1), t.nextSlide = t.randomMap[t.randomIndex];
        else if (t.random) t.nextSlide = t.randomMap[t.randomIndex];
        else if (t.nextSlide = t.currSlide + i, t.nextSlide < 0) {
            if (t.nowrap) return !1;
            t.nextSlide = u.length - 1
        } else if (t.nextSlide >= u.length) {
            if (t.nowrap) return !1;
            t.nextSlide = 0
        }
        return f = t.onPrevNextEvent || t.prevNextClick, n.isFunction(f) && f(i > 0, t.nextSlide, u[t.nextSlide]), r(u, t, 1, i >= 0), !1
    }
    function l(t, i) {
        var r = n(i.pager);
        n.each(t, function (u, f) {
            n.fn.cycle.createPagerAnchor(u, f, r, t, i)
        }), i.updateActivePagerLink(i.pager, i.startingSlide, i.activePagerClass)
    }
    function f(t) {
        function r(n) {
            return n = parseInt(n).toString(16), n.length < 2 ? "0" + n : n
        }
        function u(t) {
            for (var i, u; t && t.nodeName.toLowerCase() != "html"; t = t.parentNode) {
                if (i = n.css(t, "background-color"), i.indexOf("rgb") >= 0) return u = i.match(/\d+/g), "#" + r(u[0]) + r(u[1]) + r(u[2]);
                if (i && i != "transparent") return i
            }
            return "#ffffff"
        }
        i("applying clearType background-color hack"), t.each(function () {
            n(this).css("background-color", u(this))
        })
    }
    var s = "2.88";
    n.support == undefined && (n.support = {
        opacity: !n.browser.msie
    }), n.fn.cycle = function (u, f) {
        var o = {
            s: this.selector,
            c: this.context
        };
        return this.length === 0 && u != "stop" ? !n.isReady && o.s ? (t("DOM not ready, queuing slideshow"), n(function () {
            n(o.s, o.c).cycle(u, f)
        }), this) : (t("terminating; zero elements found by selector" + (n.isReady ? "" : " (DOM not ready)")), this) : this.each(function () {
            var a = h(this, u, f),
                s, l;
            if (a !== !1) {
                a.updateActivePagerLink = a.updateActivePagerLink || n.fn.cycle.updateActivePagerLink, this.cycleTimeout && clearTimeout(this.cycleTimeout), this.cycleTimeout = this.cyclePause = 0;
                var p = n(this),
                    y = a.slideExpr ? n(a.slideExpr, this) : p.children(),
                    v = y.get();
                if (v.length < 2) {
                    t("terminating; too few slides: " + v.length);
                    return
                }(s = c(p, y, v, a, o), s !== !1) && (l = s.continuous ? 10 : e(v[s.currSlide], v[s.nextSlide], s, !s.rev), l && (l += s.delay || 0, l < 10 && (l = 10), i("first timeout: " + l), this.cycleTimeout = setTimeout(function () {
                    r(v, s, 0, !s.rev && !a.backwards)
                }, l)))
            }
        })
    }, n.fn.cycle.resetState = function (t, i) {
        i = i || t.fx, t.before = [], t.after = [], t.cssBefore = n.extend({}, t.original.cssBefore), t.cssAfter = n.extend({}, t.original.cssAfter), t.animIn = n.extend({}, t.original.animIn), t.animOut = n.extend({}, t.original.animOut), t.fxFn = null, n.each(t.original.before, function () {
            t.before.push(this)
        }), n.each(t.original.after, function () {
            t.after.push(this)
        });
        var r = n.fn.cycle.transitions[i];
        n.isFunction(r) && r(t.$cont, n(t.elements), t)
    }, n.fn.cycle.updateActivePagerLink = function (t, i, r) {
        n(t).each(function () {
            n(this).children().removeClass(r).eq(i).addClass(r)
        })
    }, n.fn.cycle.next = function (n) {
        u(n, n.rev ? -1 : 1)
    }, n.fn.cycle.prev = function (n) {
        u(n, n.rev ? 1 : -1)
    }, n.fn.cycle.createPagerAnchor = function (t, u, f, e, o) {
        var h, s, c;
        (n.isFunction(o.pagerAnchorBuilder) ? (h = o.pagerAnchorBuilder(t, u), i("pagerAnchorBuilder(" + t + ", el) returned: " + h)) : h = '<a href="#">' + (t + 1) + "</a>", h) && (s = n(h), s.parents("body").length === 0 && (c = [], f.length > 1 ? (f.each(function () {
            var t = s.clone(!0);
            n(this).append(t), c.push(t[0])
        }), s = n(c)) : s.appendTo(f)), o.pagerAnchors = o.pagerAnchors || [], o.pagerAnchors.push(s), s.bind(o.pagerEvent, function (i) {
            var s, f, u;
            i.preventDefault(), o.nextSlide = t, s = o.$cont[0], f = s.cycleTimeout, f && (clearTimeout(f), s.cycleTimeout = 0), u = o.onPagerEvent || o.pagerClick, n.isFunction(u) && u(o.nextSlide, e[o.nextSlide]), r(e, o, 1, o.currSlide < t)
        }), /^click/.test(o.pagerEvent) || o.allowPagerClickBubble || s.bind("click.cycle", function () {
            return !1
        }), o.pauseOnPagerHover && s.hover(function () {
            o.$cont[0].cyclePause++
        }, function () {
            o.$cont[0].cyclePause--
        }))
    }, n.fn.cycle.hopsFromLast = function (n, t) {
        var u, i = n.lastSlide,
            r = n.currSlide;
        return u = t ? r > i ? r - i : n.slideCount - i : r < i ? i - r : i + n.slideCount - r
    }, n.fn.cycle.commonReset = function (t, i, r, u, f, e) {
        n(r.elements).not(t).hide(), r.cssBefore.opacity = 1, r.cssBefore.display = "block", u !== !1 && i.cycleW > 0 && (r.cssBefore.width = i.cycleW), f !== !1 && i.cycleH > 0 && (r.cssBefore.height = i.cycleH), r.cssAfter = r.cssAfter || {}, r.cssAfter.display = "none", n(t).css("zIndex", r.slideCount + (e === !0 ? 1 : 0)), n(i).css("zIndex", r.slideCount + (e === !0 ? 0 : 1))
    }, n.fn.cycle.custom = function (t, i, r, u, f, e) {
        var l = n(t),
            a = n(i),
            o = r.speedIn,
            s = r.speedOut,
            v = r.easeIn,
            c = r.easeOut,
            h;
        a.css(r.cssBefore), e && (o = typeof e == "number" ? s = e : s = 1, v = c = null), h = function () {
            a.animate(r.animIn, o, v, u)
        }, l.animate(r.animOut, s, c, function () {
            r.cssAfter && l.css(r.cssAfter), r.sync || h()
        }), r.sync && h()
    }, n.fn.cycle.transitions = {
        fade: function (t, i, r) {
            i.not(":eq(" + r.currSlide + ")").css("opacity", 0), r.before.push(function (t, i, r) {
                n.fn.cycle.commonReset(t, i, r), r.cssBefore.opacity = 0
            }), r.animIn = {
                opacity: 1
            }, r.animOut = {
                opacity: 0
            }, r.cssBefore = {
                top: 0,
                left: 0
            }
        }
    }, n.fn.cycle.ver = function () {
        return s
    }, n.fn.cycle.defaults = {
        fx: "fade",
        timeout: 4e3,
        timeoutFn: null,
        continuous: 0,
        speed: 1e3,
        speedIn: null,
        speedOut: null,
        next: null,
        prev: null,
        onPrevNextEvent: null,
        prevNextEvent: "click.cycle",
        pager: null,
        onPagerEvent: null,
        pagerEvent: "click.cycle",
        allowPagerClickBubble: !1,
        pagerAnchorBuilder: null,
        before: null,
        after: null,
        end: null,
        easing: null,
        easeIn: null,
        easeOut: null,
        shuffle: null,
        animIn: null,
        animOut: null,
        cssBefore: null,
        cssAfter: null,
        fxFn: null,
        height: "auto",
        startingSlide: 0,
        sync: 1,
        random: 0,
        fit: 0,
        containerResize: 1,
        pause: 0,
        pauseOnPagerHover: 0,
        autostop: 0,
        autostopCount: 0,
        delay: 0,
        slideExpr: null,
        cleartype: !n.support.opacity,
        cleartypeNoBg: !1,
        nowrap: 0,
        fastOnEvent: 0,
        randomizeEffects: 1,
        rev: 0,
        manualTrump: !0,
        requeueOnImageNotLoaded: !0,
        requeueTimeout: 250,
        activePagerClass: "activeSlide",
        updateActivePagerLink: null,
        backwards: !1
    }
}(jQuery),
function (n) {
    n.fn.cycle.transitions.none = function (t, i, r) {
        r.fxFn = function (t, i, r, u) {
            n(i).show(), n(t).hide(), u()
        }
    }, n.fn.cycle.transitions.scrollUp = function (t, i, r) {
        t.css("overflow", "hidden"), r.before.push(n.fn.cycle.commonReset);
        var u = t.height();
        r.cssBefore = {
            top: u,
            left: 0
        }, r.cssFirst = {
            top: 0
        }, r.animIn = {
            top: 0
        }, r.animOut = {
            top: -u
        }
    }, n.fn.cycle.transitions.scrollDown = function (t, i, r) {
        t.css("overflow", "hidden"), r.before.push(n.fn.cycle.commonReset);
        var u = t.height();
        r.cssFirst = {
            top: 0
        }, r.cssBefore = {
            top: -u,
            left: 0
        }, r.animIn = {
            top: 0
        }, r.animOut = {
            top: u
        }
    }, n.fn.cycle.transitions.scrollLeft = function (t, i, r) {
        t.css("overflow", "hidden"), r.before.push(n.fn.cycle.commonReset);
        var u = t.width();
        r.cssFirst = {
            left: 0
        }, r.cssBefore = {
            left: u,
            top: 0
        }, r.animIn = {
            left: 0
        }, r.animOut = {
            left: 0 - u
        }
    }, n.fn.cycle.transitions.scrollRight = function (t, i, r) {
        t.css("overflow", "hidden"), r.before.push(n.fn.cycle.commonReset);
        var u = t.width();
        r.cssFirst = {
            left: 0
        }, r.cssBefore = {
            left: -u,
            top: 0
        }, r.animIn = {
            left: 0
        }, r.animOut = {
            left: u
        }
    }, n.fn.cycle.transitions.scrollHorz = function (t, i, r) {
        t.css("overflow", "hidden").width(), r.before.push(function (t, i, r, u) {
            n.fn.cycle.commonReset(t, i, r), r.cssBefore.left = u ? i.cycleW - 1 : 1 - i.cycleW, r.animOut.left = u ? -t.cycleW : t.cycleW
        }), r.cssFirst = {
            left: 0
        }, r.cssBefore = {
            top: 0
        }, r.animIn = {
            left: 0
        }, r.animOut = {
            top: 0
        }
    }, n.fn.cycle.transitions.scrollVert = function (t, i, r) {
        t.css("overflow", "hidden"), r.before.push(function (t, i, r, u) {
            n.fn.cycle.commonReset(t, i, r), r.cssBefore.top = u ? 1 - i.cycleH : i.cycleH - 1, r.animOut.top = u ? t.cycleH : -t.cycleH
        }), r.cssFirst = {
            top: 0
        }, r.cssBefore = {
            left: 0
        }, r.animIn = {
            top: 0
        }, r.animOut = {
            left: 0
        }
    }, n.fn.cycle.transitions.slideX = function (t, i, r) {
        r.before.push(function (t, i, r) {
            n(r.elements).not(t).hide(), n.fn.cycle.commonReset(t, i, r, !1, !0), r.animIn.width = i.cycleW
        }), r.cssBefore = {
            left: 0,
            top: 0,
            width: 0
        }, r.animIn = {
            width: "show"
        }, r.animOut = {
            width: 0
        }
    }, n.fn.cycle.transitions.slideY = function (t, i, r) {
        r.before.push(function (t, i, r) {
            n(r.elements).not(t).hide(), n.fn.cycle.commonReset(t, i, r, !0, !1), r.animIn.height = i.cycleH
        }), r.cssBefore = {
            left: 0,
            top: 0,
            height: 0
        }, r.animIn = {
            height: "show"
        }, r.animOut = {
            height: 0
        }
    }, n.fn.cycle.transitions.shuffle = function (t, i, r) {
        var u, f = t.css("overflow", "visible").width();
        for (i.css({
            left: 0,
            top: 0
        }), r.before.push(function (t, i, r) {
            n.fn.cycle.commonReset(t, i, r, !0, !0, !0)
        }), r.speedAdjusted || (r.speed = r.speed / 2, r.speedAdjusted = !0), r.random = 0, r.shuffle = r.shuffle || {
            left: -f,
            top: 15
        }, r.els = [], u = 0; u < i.length; u++) r.els.push(i[u]);
        for (u = 0; u < r.currSlide; u++) r.els.push(r.els.shift());
        r.fxFn = function (t, i, r, u, f) {
            var o = f ? n(t) : n(i),
                e;
            n(i).css(r.cssBefore), e = r.slideCount, o.animate(r.shuffle, r.speedIn, r.easeIn, function () {
                for (var l = n.fn.cycle.hopsFromLast(r, f), i, s, c, h = 0; h < l; h++) f ? r.els.push(r.els.shift()) : r.els.unshift(r.els.pop());
                if (f) for (i = 0, s = r.els.length; i < s; i++) n(r.els[i]).css("z-index", s - i + e);
                else c = n(t).css("z-index"), o.css("z-index", parseInt(c) + 1 + e);
                o.animate({
                    left: 0,
                    top: 0
                }, r.speedOut, r.easeOut, function () {
                    n(f ? this : t).hide(), u && u()
                })
            })
        }, r.cssBefore = {
            display: "block",
            opacity: 1,
            top: 0,
            left: 0
        }
    }, n.fn.cycle.transitions.turnUp = function (t, i, r) {
        r.before.push(function (t, i, r) {
            n.fn.cycle.commonReset(t, i, r, !0, !1), r.cssBefore.top = i.cycleH, r.animIn.height = i.cycleH
        }), r.cssFirst = {
            top: 0
        }, r.cssBefore = {
            left: 0,
            height: 0
        }, r.animIn = {
            top: 0
        }, r.animOut = {
            height: 0
        }
    }, n.fn.cycle.transitions.turnDown = function (t, i, r) {
        r.before.push(function (t, i, r) {
            n.fn.cycle.commonReset(t, i, r, !0, !1), r.animIn.height = i.cycleH, r.animOut.top = t.cycleH
        }), r.cssFirst = {
            top: 0
        }, r.cssBefore = {
            left: 0,
            top: 0,
            height: 0
        }, r.animOut = {
            height: 0
        }
    }, n.fn.cycle.transitions.turnLeft = function (t, i, r) {
        r.before.push(function (t, i, r) {
            n.fn.cycle.commonReset(t, i, r, !1, !0), r.cssBefore.left = i.cycleW, r.animIn.width = i.cycleW
        }), r.cssBefore = {
            top: 0,
            width: 0
        }, r.animIn = {
            left: 0
        }, r.animOut = {
            width: 0
        }
    }, n.fn.cycle.transitions.turnRight = function (t, i, r) {
        r.before.push(function (t, i, r) {
            n.fn.cycle.commonReset(t, i, r, !1, !0), r.animIn.width = i.cycleW, r.animOut.left = t.cycleW
        }), r.cssBefore = {
            top: 0,
            left: 0,
            width: 0
        }, r.animIn = {
            left: 0
        }, r.animOut = {
            width: 0
        }
    }, n.fn.cycle.transitions.zoom = function (t, i, r) {
        r.before.push(function (t, i, r) {
            n.fn.cycle.commonReset(t, i, r, !1, !1, !0), r.cssBefore.top = i.cycleH / 2, r.cssBefore.left = i.cycleW / 2, r.animIn = {
                top: 0,
                left: 0,
                width: i.cycleW,
                height: i.cycleH
            }, r.animOut = {
                width: 0,
                height: 0,
                top: t.cycleH / 2,
                left: t.cycleW / 2
            }
        }), r.cssFirst = {
            top: 0,
            left: 0
        }, r.cssBefore = {
            width: 0,
            height: 0
        }
    }, n.fn.cycle.transitions.fadeZoom = function (t, i, r) {
        r.before.push(function (t, i, r) {
            n.fn.cycle.commonReset(t, i, r, !1, !1), r.cssBefore.left = i.cycleW / 2, r.cssBefore.top = i.cycleH / 2, r.animIn = {
                top: 0,
                left: 0,
                width: i.cycleW,
                height: i.cycleH
            }
        }), r.cssBefore = {
            width: 0,
            height: 0
        }, r.animOut = {
            opacity: 0
        }
    }, n.fn.cycle.transitions.blindX = function (t, i, r) {
        var u = t.css("overflow", "hidden").width();
        r.before.push(function (t, i, r) {
            n.fn.cycle.commonReset(t, i, r), r.animIn.width = i.cycleW, r.animOut.left = t.cycleW
        }), r.cssBefore = {
            left: u,
            top: 0
        }, r.animIn = {
            left: 0
        }, r.animOut = {
            left: u
        }
    }, n.fn.cycle.transitions.blindY = function (t, i, r) {
        var u = t.css("overflow", "hidden").height();
        r.before.push(function (t, i, r) {
            n.fn.cycle.commonReset(t, i, r), r.animIn.height = i.cycleH, r.animOut.top = t.cycleH
        }), r.cssBefore = {
            top: u,
            left: 0
        }, r.animIn = {
            top: 0
        }, r.animOut = {
            top: u
        }
    }, n.fn.cycle.transitions.blindZ = function (t, i, r) {
        var f = t.css("overflow", "hidden").height(),
            u = t.width();
        r.before.push(function (t, i, r) {
            n.fn.cycle.commonReset(t, i, r), r.animIn.height = i.cycleH, r.animOut.top = t.cycleH
        }), r.cssBefore = {
            top: f,
            left: u
        }, r.animIn = {
            top: 0,
            left: 0
        }, r.animOut = {
            top: f,
            left: u
        }
    }, n.fn.cycle.transitions.growX = function (t, i, r) {
        r.before.push(function (t, i, r) {
            n.fn.cycle.commonReset(t, i, r, !1, !0), r.cssBefore.left = this.cycleW / 2, r.animIn = {
                left: 0,
                width: this.cycleW
            }, r.animOut = {
                left: 0
            }
        }), r.cssBefore = {
            width: 0,
            top: 0
        }
    }, n.fn.cycle.transitions.growY = function (t, i, r) {
        r.before.push(function (t, i, r) {
            n.fn.cycle.commonReset(t, i, r, !0, !1), r.cssBefore.top = this.cycleH / 2, r.animIn = {
                top: 0,
                height: this.cycleH
            }, r.animOut = {
                top: 0
            }
        }), r.cssBefore = {
            height: 0,
            left: 0
        }
    }, n.fn.cycle.transitions.curtainX = function (t, i, r) {
        r.before.push(function (t, i, r) {
            n.fn.cycle.commonReset(t, i, r, !1, !0, !0), r.cssBefore.left = i.cycleW / 2, r.animIn = {
                left: 0,
                width: this.cycleW
            }, r.animOut = {
                left: t.cycleW / 2,
                width: 0
            }
        }), r.cssBefore = {
            top: 0,
            width: 0
        }
    }, n.fn.cycle.transitions.curtainY = function (t, i, r) {
        r.before.push(function (t, i, r) {
            n.fn.cycle.commonReset(t, i, r, !0, !1, !0), r.cssBefore.top = i.cycleH / 2, r.animIn = {
                top: 0,
                height: i.cycleH
            }, r.animOut = {
                top: t.cycleH / 2,
                height: 0
            }
        }), r.cssBefore = {
            left: 0,
            height: 0
        }
    }, n.fn.cycle.transitions.cover = function (t, i, r) {
        var u = r.direction || "left",
            e = t.css("overflow", "hidden").width(),
            f = t.height();
        r.before.push(function (t, i, r) {
            n.fn.cycle.commonReset(t, i, r), u == "right" ? r.cssBefore.left = -e : u == "up" ? r.cssBefore.top = f : u == "down" ? r.cssBefore.top = -f : r.cssBefore.left = e
        }), r.animIn = {
            left: 0,
            top: 0
        }, r.animOut = {
            opacity: 1
        }, r.cssBefore = {
            top: 0,
            left: 0
        }
    }, n.fn.cycle.transitions.uncover = function (t, i, r) {
        var u = r.direction || "left",
            e = t.css("overflow", "hidden").width(),
            f = t.height();
        r.before.push(function (t, i, r) {
            n.fn.cycle.commonReset(t, i, r, !0, !0, !0), u == "right" ? r.animOut.left = e : u == "up" ? r.animOut.top = -f : u == "down" ? r.animOut.top = f : r.animOut.left = -e
        }), r.animIn = {
            left: 0,
            top: 0
        }, r.animOut = {
            opacity: 1
        }, r.cssBefore = {
            top: 0,
            left: 0
        }
    }, n.fn.cycle.transitions.toss = function (t, i, r) {
        var f = t.css("overflow", "visible").width(),
            u = t.height();
        r.before.push(function (t, i, r) {
            n.fn.cycle.commonReset(t, i, r, !0, !0, !0), r.animOut.left || r.animOut.top ? r.animOut.opacity = 0 : r.animOut = {
                left: f * 2,
                top: -u / 2,
                opacity: 0
            }
        }), r.cssBefore = {
            left: 0,
            top: 0
        }, r.animIn = {
            left: 0
        }
    }, n.fn.cycle.transitions.wipe = function (t, i, r) {
        var f = t.css("overflow", "hidden").width(),
            u = t.height(),
            e, l, a;
        r.cssBefore = r.cssBefore || {}, r.clip && (/l2r/.test(r.clip) ? e = "rect(0px 0px " + u + "px 0px)" : /r2l/.test(r.clip) ? e = "rect(0px " + f + "px " + u + "px " + f + "px)" : /t2b/.test(r.clip) ? e = "rect(0px " + f + "px 0px 0px)" : /b2t/.test(r.clip) ? e = "rect(" + u + "px " + f + "px " + u + "px 0px)" : /zoom/.test(r.clip) && (l = parseInt(u / 2), a = parseInt(f / 2), e = "rect(" + l + "px " + a + "px " + l + "px " + a + "px)")), r.cssBefore.clip = r.cssBefore.clip || e || "rect(0px 0px 0px 0px)";
        var o = r.cssBefore.clip.match(/(\d+)/g),
            v = parseInt(o[0]),
            s = parseInt(o[1]),
            c = parseInt(o[2]),
            h = parseInt(o[3]);
        r.before.push(function (t, i, r) {
            var l, a, e, o;
            t != i && (l = n(t), a = n(i), n.fn.cycle.commonReset(t, i, r, !0, !0, !1), r.cssAfter.display = "block", e = 1, o = parseInt(r.speedIn / 13) - 1, function y() {
                var i = v ? v - parseInt(e * (v / o)) : 0,
                    r = h ? h - parseInt(e * (h / o)) : 0,
                    n = c < u ? c + parseInt(e * ((u - c) / o || 1)) : u,
                    t = s < f ? s + parseInt(e * ((f - s) / o || 1)) : f;
                a.css({
                    clip: "rect(" + i + "px " + t + "px " + n + "px " + r + "px)"
                }), e++ <= o ? setTimeout(y, 13) : l.css("display", "none")
            }())
        }), r.cssBefore = {
            display: "block",
            opacity: 1,
            top: 0,
            left: 0
        }, r.animIn = {
            left: 0
        }, r.animOut = {
            left: 0
        }
    }
}(jQuery), $(document).ready(function () {
    var n = $("#culture").val();
    $("#head .menu ul.cssdropdown li ul").hover(function () {
        $(this).siblings("a").addClass("selected")
    }, function () {
        $(this).siblings("a").removeClass("selected")
    }), Cufon.replace("h1,h2,h3,h4,h5", {
        fontFamily: "Helvetica Neue LT Pro",
        hover: {
            color: "#000000"
        }
    }), Cufon.replace(".news_content h2", {
        fontFamily: "MyriadPro-Condensed",
        hover: {
            color: "#000000"
        }
    }), $("#ebso_haberler .cycle").cycle({
        fx: "fade",
        timeout: 5500,
        pager: "#ebso_haberler .slide-controls",
        cleartype: !0,
        cleartypeNoBg: !0
    }), $("#ebso_duyurular .cycle").cycle({
        fx: "fade",
        timeout: 5500,
        pager: "#ebso_duyurular .slide-controls",
        cleartype: !0,
        cleartypeNoBg: !0
    }), $("#guncel_duyurular .cycle").cycle({
        fx: "fade",
        timeout: 5500,
        pager: "#guncel_duyurular .slide-controls",
        cleartype: !0,
        cleartypeNoBg: !0
    }), $("#ebso_toolbar .ebso_tool li:last").addClass("last"), $(".ajanda_panel p:gt(1)").addClass("last"), $(".datepicker").datepicker({
        dateFormat: "dd.mm.yy"
        //}), init_fields(), setTimeout(function () {
    }), setTimeout(function () {
        equalHeight($(".main_equal"))
    }, 400), $(".cssdropdown>li>ul>li").hover(function () {
        var t = $(this).index(),
            n = $(this).width();
        $("ul.submenu ul.submenu").css("margin-top", t * 30 - 10 + "px", "important"), $("ul.submenu ul.submenu").css("left", n - 20 + "px", "important")
    }), setTimeout(function () {
        equalHeight($(".pageheight"))
    }, 400), $("#header_search").keypress(function (t) {
        var i = $(this).val().replace(/\./g, "").trim();
        if (t.keyCode == 13) return i == "" ? !1 : (window.location.href = n == "tr-TR" ? "/tr/arama/" + encodeURI(i) : "/en/search/" + encodeURI(i), !1)
    }), $("#login_button").click(function () {
        var i = $("#login_email"),
            t = $("#login_pass");
        i.val() != i.attr("title") && t.val() != t.attr("title") ? DoLogin(n) : n == "tr-TR" ? MessageBox("alert", n, "Lütfen mail ve şifrenizi giriniz.") : MessageBox("alert", n, "Please fill in the required fields")
    }), $("#lnkbtnsifremiunuttum").click(function () {
        $.fancybox({
            titlePosition: "over",
            overlayColor: "#000",
            overlayOpacity: .8,
            overlayShow: !0,
            type: "inline",
            content: "#rememberDialog"
        })
    }), $("#memmberlist a").click(function () {
        CompanyInfo($(this).parent().parent().attr("id")), $.fancybox({
            titlePosition: "over",
            overlayColor: "#000",
            overlayOpacity: .8,
            overlayShow: !0,
            type: "inline",
            content: "#memberDetail"
        })
    }), $("#rememberbutton").click(function () {
        var t = $("#companyIdorName").val().trim();
        t != "" && RememberPassword(n, t)
    }), $("#activitylist a").click(function () {
        ActivityInfo($(this).parent().attr("id")), $.fancybox({
            titlePosition: "over",
            overlayColor: "#000",
            overlayOpacity: .8,
            overlayShow: !0,
            type: "inline",
            content: "#ActivityDetail"
        })
    }), $("#ajanda-toplanti p a").click(function () {
        ActivityInfo($(this).attr("id")), $.fancybox({
            titlePosition: "over",
            overlayColor: "#000",
            overlayOpacity: .8,
            overlayShow: !0,
            type: "inline",
            content: "#ActivityDetail"
        })
    })
}),
function (n) {
    var h, y, w, r, rt, e, it, o, nt, g, a = 0,
        i = {}, l = [],
        c = 0,
        t = {}, s = [],
        ft = null,
        p = new Image,
        ot = /\.(jpg|gif|png|bmp|jpeg)(.*)?$/i,
        kt = /[^\.]\.(swf)\s*$/i,
        vt, at = 1,
        k = 0,
        b = "",
        v, f, u = !1,
        d = n.extend(n("<div/>")[0], {
            prop: 0
        }),
        lt = n.browser.msie && n.browser.version < 7 && !window.XMLHttpRequest,
        yt = function () {
            y.hide(), p.onerror = p.onload = null, ft && ft.abort(), h.empty()
        }, pt = function () {
            if (!1 === i.onError(l, a, i)) {
                y.hide(), u = !1;
                return
            }
            i.titleShow = !1, i.width = "auto", i.height = "auto", h.html('<p id="fancybox-error">The requested content cannot be loaded.<br />Please try again later.</p>'), tt()
        }, ut = function () {
            var r = l[a],
                t, f, s, c, v, o;
            if (yt(), i = n.extend({}, n.fn.fancybox.defaults, typeof n(r).data("fancybox") == "undefined" ? i : n(r).data("fancybox")), o = i.onStart(l, a, i), o === !1) {
                u = !1;
                return
            }
            if (typeof o == "object" && (i = n.extend(i, o)), s = i.title || (r.nodeName ? n(r).attr("title") : r.title) || "", r.nodeName && !i.orig && (i.orig = n(r).children("img:first").length ? n(r).children("img:first") : n(r)), s === "" && i.orig && i.titleFromAlt && (s = i.orig.attr("alt")), t = i.href || (r.nodeName ? n(r).attr("href") : r.href) || null, (/^(?:javascript)/i.test(t) || t == "#") && (t = null), i.type ? (f = i.type, t || (t = i.content)) : i.content ? f = "html" : t && (f = t.match(ot) ? "image" : t.match(kt) ? "swf" : n(r).hasClass("iframe") ? "iframe" : t.indexOf("#") === 0 ? "inline" : "ajax"), !f) {
                pt();
                return
            }
            f == "inline" && (r = t.substr(t.indexOf("#")), f = n(r).length > 0 ? "inline" : "ajax"), i.type = f, i.href = t, i.title = s, i.autoDimensions && (i.type == "html" || i.type == "inline" || i.type == "ajax" ? (i.width = "auto", i.height = "auto") : i.autoDimensions = !1), i.modal && (i.overlayShow = !0, i.hideOnOverlayClick = !1, i.hideOnContentClick = !1, i.enableEscapeButton = !1, i.showCloseButton = !1), i.padding = parseInt(i.padding, 10), i.margin = parseInt(i.margin, 10), h.css("padding", i.padding + i.margin), n(".fancybox-inline-tmp").unbind("fancybox-cancel").bind("fancybox-change", function () {
                n(this).replaceWith(e.children())
            });
            switch (f) {
                case "html":
                    h.html(i.content), tt();
                    break;
                case "inline":
                    if (n(r).parent().is("#fancybox-content") === !0) {
                        u = !1;
                        return
                    }
                    n('<div class="fancybox-inline-tmp" />').hide().insertBefore(n(r)).bind("fancybox-cleanup", function () {
                        n(this).replaceWith(e.children())
                    }).bind("fancybox-cancel", function () {
                        n(this).replaceWith(h.children())
                    }), n(r).appendTo(h), tt();
                    break;
                case "image":
                    u = !1, n.fancybox.showActivity(), p = new Image, p.onerror = function () {
                        pt()
                    }, p.onload = function () {
                        u = !0, p.onerror = p.onload = null, ti()
                    }, p.src = t;
                    break;
                case "swf":
                    i.scrolling = "no", c = '<object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" width="' + i.width + '" height="' + i.height + '"><param name="movie" value="' + t + '"></param>', v = "", n.each(i.swf, function (n, t) {
                        c += '<param name="' + n + '" value="' + t + '"></param>', v += " " + n + '="' + t + '"'
                    }), c += '<embed src="' + t + '" type="application/x-shockwave-flash" width="' + i.width + '" height="' + i.height + '"' + v + "></embed></object>", h.html(c), tt();
                    break;
                case "ajax":
                    u = !1, n.fancybox.showActivity(), i.ajax.win = i.ajax.success, ft = n.ajax(n.extend({}, i.ajax, {
                        url: t,
                        data: i.ajax.data || {},
                        error: function (n) {
                            n.status > 0 && pt()
                        },
                        success: function (n, r, u) {
                            var f = typeof u == "object" ? u : ft;
                            if (f.status == 200) {
                                if (typeof i.ajax.win == "function") {
                                    if (o = i.ajax.win(t, n, r, u), o === !1) {
                                        y.hide();
                                        return
                                    }(typeof o == "string" || typeof o == "object") && (n = o)
                                }
                                h.html(n), tt()
                            }
                        }
                    }));
                    break;
                case "iframe":
                    st()
            }
        }, tt = function () {
            var r = i.width,
                t = i.height;
            r = r.toString().indexOf("%") > -1 ? parseInt((n(window).width() - i.margin * 2) * parseFloat(r) / 100, 10) + "px" : r == "auto" ? "auto" : r + "px", t = t.toString().indexOf("%") > -1 ? parseInt((n(window).height() - i.margin * 2) * parseFloat(t) / 100, 10) + "px" : t == "auto" ? "auto" : t + "px", h.wrapInner('<div style="width:' + r + ";height:" + t + ";overflow: " + (i.scrolling == "auto" ? "auto" : i.scrolling == "yes" ? "scroll" : "hidden") + ';position:relative;"></div>'), i.width = h.width(), i.height = h.height(), st()
        }, ti = function () {
            i.width = p.width, i.height = p.height, n("<img />").attr({
                id: "fancybox-img",
                src: p.src,
                alt: i.title
            }).appendTo(h), st()
        }, st = function () {
            var p, b;
            if (y.hide(), r.is(":visible") && !1 === t.onCleanup(s, c, t)) {
                n.event.trigger("fancybox-cancel"), u = !1;
                return
            }
            if (u = !0, n(e.add(w)).unbind(), n(window).unbind("resize.fb scroll.fb"), n(document).unbind("keydown.fb"), r.is(":visible") && t.titlePosition !== "outside" && r.css("height", r.height()), s = l, c = a, t = i, t.overlayShow) {
                if (w.css({
                    "background-color": t.overlayColor,
                    opacity: t.overlayOpacity,
                    cursor: t.hideOnOverlayClick ? "pointer" : "auto",
                    height: n(document).height()
                }), !w.is(":visible")) {
                    if (lt) n("select:not(#fancybox-tmp select)").filter(function () {
                        return this.style.visibility !== "hidden"
                    }).css({
                        visibility: "hidden"
                    }).one("fancybox-cleanup", function () {
                        this.style.visibility = "inherit"
                    });
                    w.show()
                }
            } else w.hide();
            if (f = gt(), ii(), r.is(":visible")) {
                n(it.add(nt).add(g)).hide(), p = r.position(), v = {
                    top: p.top,
                    left: p.left,
                    width: r.width(),
                    height: r.height()
                }, b = v.width == f.width && v.height == f.height, e.fadeTo(t.changeFade, .3, function () {
                    var r = function () {
                        e.html(h.contents()).fadeTo(t.changeFade, 1, ht)
                    };
                    n.event.trigger("fancybox-change"), e.empty().removeAttr("filter").css({
                        "border-width": t.padding,
                        width: f.width - t.padding * 2,
                        height: i.autoDimensions ? "auto" : f.height - k - t.padding * 2
                    }), b ? r() : (d.prop = 0, n(d).animate({
                        prop: 1
                    }, {
                        duration: t.changeSpeed,
                        easing: t.easingChange,
                        step: ct,
                        complete: r
                    }))
                });
                return
            }
            if (r.removeAttr("style"), e.css("border-width", t.padding), t.transitionIn == "elastic") {
                v = wt(), e.html(h.contents()), r.show(), t.opacity && (f.opacity = 0), d.prop = 0, n(d).animate({
                    prop: 1
                }, {
                    duration: t.speedIn,
                    easing: t.easingIn,
                    step: ct,
                    complete: ht
                });
                return
            }
            t.titlePosition == "inside" && k > 0 && o.show(), e.css({
                width: f.width - t.padding * 2,
                height: i.autoDimensions ? "auto" : f.height - k - t.padding * 2
            }).html(h.contents()), r.css(f).fadeIn(t.transitionIn == "none" ? 0 : t.speedIn, ht)
        }, ni = function (n) {
            return n && n.length ? t.titlePosition == "float" ? '<table id="fancybox-title-float-wrap" cellpadding="0" cellspacing="0"><tr><td id="fancybox-title-float-left"></td><td id="fancybox-title-float-main">' + n + '</td><td id="fancybox-title-float-right"></td></tr></table>' : '<div id="fancybox-title-' + t.titlePosition + '">' + n + "</div>" : !1
        }, ii = function () {
            if (b = t.title || "", k = 0, o.empty().removeAttr("style").removeClass(), t.titleShow === !1) {
                o.hide();
                return
            }
            if (b = n.isFunction(t.titleFormat) ? t.titleFormat(b, s, c, t) : ni(b), !b || b === "") {
                o.hide();
                return
            }
            o.addClass("fancybox-title-" + t.titlePosition).html(b).appendTo("body").show();
            switch (t.titlePosition) {
                case "inside":
                    o.css({
                        width: f.width - t.padding * 2,
                        marginLeft: t.padding,
                        marginRight: t.padding
                    }), k = o.outerHeight(!0), o.appendTo(rt), f.height += k;
                    break;
                case "over":
                    o.css({
                        marginLeft: t.padding,
                        width: f.width - t.padding * 2,
                        bottom: t.padding
                    }).appendTo(rt);
                    break;
                case "float":
                    o.css("left", parseInt((o.width() - f.width - 40) / 2, 10) * -1).appendTo(r);
                    break;
                default:
                    o.css({
                        width: f.width - t.padding * 2,
                        paddingLeft: t.padding,
                        paddingRight: t.padding
                    }).appendTo(r)
            }
            o.hide()
        }, ui = function () {
            if ((t.enableEscapeButton || t.enableKeyboardNav) && n(document).bind("keydown.fb", function (i) {
                i.keyCode == 27 && t.enableEscapeButton ? (i.preventDefault(), n.fancybox.close()) : (i.keyCode == 37 || i.keyCode == 39) && t.enableKeyboardNav && i.target.tagName !== "INPUT" && i.target.tagName !== "TEXTAREA" && i.target.tagName !== "SELECT" && (i.preventDefault(), n.fancybox[i.keyCode == 37 ? "prev" : "next"]())
            }), !t.showNavArrows) {
                nt.hide(), g.hide();
                return
            }(t.cyclic && s.length > 1 || c !== 0) && nt.show(), (t.cyclic && s.length > 1 || c != s.length - 1) && g.show()
        }, ht = function () {
            n.support.opacity || (e.get(0).style.removeAttribute("filter"), r.get(0).style.removeAttribute("filter")), i.autoDimensions && e.css("height", "auto"), r.css("height", "auto"), b && b.length && o.show(), t.showCloseButton && it.show(), ui(), t.hideOnContentClick && e.bind("click", n.fancybox.close), t.hideOnOverlayClick && w.bind("click", n.fancybox.close), n(window).bind("resize.fb", n.fancybox.resize), t.centerOnScroll && n(window).bind("scroll.fb", n.fancybox.center), t.type == "iframe" && n('<iframe id="fancybox-frame" name="fancybox-frame' + +new Date + '" frameborder="0" hspace="0" ' + (n.browser.msie ? 'allowtransparency="true""' : "") + ' scrolling="' + i.scrolling + '" src="' + t.href + '"></iframe>').appendTo(e), r.show(), u = !1, n.fancybox.center();
            t.onComplete(s, c, t);
            ri()
        }, ri = function () {
            var n, t;
            s.length - 1 > c && (n = s[c + 1].href, typeof n != "undefined" && n.match(ot) && (t = new Image, t.src = n)), c > 0 && (n = s[c - 1].href, typeof n != "undefined" && n.match(ot) && (t = new Image, t.src = n))
        }, ct = function (n) {
            var i = {
                width: parseInt(v.width + (f.width - v.width) * n, 10),
                height: parseInt(v.height + (f.height - v.height) * n, 10),
                top: parseInt(v.top + (f.top - v.top) * n, 10),
                left: parseInt(v.left + (f.left - v.left) * n, 10)
            };
            typeof f.opacity != "undefined" && (i.opacity = n < .5 ? .5 : n), r.css(i), e.css({
                width: i.width - t.padding * 2,
                height: i.height - k * n - t.padding * 2
            })
        }, et = function () {
            return [n(window).width() - t.margin * 2, n(window).height() - t.margin * 2, n(document).scrollLeft() + t.margin, n(document).scrollTop() + t.margin]
        }, gt = function () {
            var r = et(),
                n = {}, e = t.autoScale,
                u = t.padding * 2,
                f;
            return n.width = t.width.toString().indexOf("%") > -1 ? parseInt(r[0] * parseFloat(t.width) / 100, 10) : t.width + u, n.height = t.height.toString().indexOf("%") > -1 ? parseInt(r[1] * parseFloat(t.height) / 100, 10) : t.height + u, e && (n.width > r[0] || n.height > r[1]) && (i.type == "image" || i.type == "swf" ? (f = t.width / t.height, n.width > r[0] && (n.width = r[0], n.height = parseInt((n.width - u) / f + u, 10)), n.height > r[1] && (n.height = r[1], n.width = parseInt((n.height - u) * f + u, 10))) : (n.width = Math.min(n.width, r[0]), n.height = Math.min(n.height, r[1]))), n.top = parseInt(Math.max(r[3] - 20, r[3] + (r[1] - n.height - 40) * .5), 10), n.left = parseInt(Math.max(r[2] - 20, r[2] + (r[0] - n.width - 40) * .5), 10), n
        }, dt = function (n) {
            var t = n.offset();
            return t.top += parseInt(n.css("paddingTop"), 10) || 0, t.left += parseInt(n.css("paddingLeft"), 10) || 0, t.top += parseInt(n.css("border-top-width"), 10) || 0, t.left += parseInt(n.css("border-left-width"), 10) || 0, t.width = n.width(), t.height = n.height(), t
        }, wt = function () {
            var f = i.orig ? n(i.orig) : !1,
                e = {}, r, u;
            return f && f.length ? (r = dt(f), e = {
                width: r.width + t.padding * 2,
                height: r.height + t.padding * 2,
                top: r.top - t.padding - 20,
                left: r.left - t.padding - 20
            }) : (u = et(), e = {
                width: t.padding * 2,
                height: t.padding * 2,
                top: parseInt(u[3] + u[1] * .5, 10),
                left: parseInt(u[2] + u[0] * .5, 10)
            }), e
        }, bt = function () {
            if (!y.is(":visible")) {
                clearInterval(vt);
                return
            }
            n("div", y).css("top", at * -40 + "px"), at = (at + 1) % 12
        };
    n.fn.fancybox = function (t) {
        return n(this).length ? (n(this).data("fancybox", n.extend({}, t, n.metadata ? n(this).metadata() : {})).unbind("click.fb").bind("click.fb", function (t) {
            if (t.preventDefault(), !u) {
                u = !0, n(this).blur(), l = [], a = 0;
                var i = n(this).attr("rel") || "";
                i && i != "" && i !== "nofollow" ? (l = n("a[rel=" + i + "], area[rel=" + i + "]"), a = l.index(this)) : l.push(this), ut();
                return
            }
        }), this) : this
    }, n.fancybox = function (t) {
        var r, i, f;
        if (!u) {
            if (u = !0, r = typeof arguments[1] != "undefined" ? arguments[1] : {}, l = [], a = parseInt(r.index, 10) || 0, n.isArray(t)) {
                for (i = 0, f = t.length; i < f; i++) typeof t[i] == "object" ? n(t[i]).data("fancybox", n.extend({}, r, t[i])) : t[i] = n({}).data("fancybox", n.extend({
                    content: t[i]
                }, r));
                l = jQuery.merge(l, t)
            } else typeof t == "object" ? n(t).data("fancybox", n.extend({}, r, t)) : t = n({}).data("fancybox", n.extend({
                content: t
            }, r)), l.push(t);
            (a > l.length || a < 0) && (a = 0), ut()
        }
    }, n.fancybox.showActivity = function () {
        clearInterval(vt), y.show(), vt = setInterval(bt, 66)
    }, n.fancybox.hideActivity = function () {
        y.hide()
    }, n.fancybox.next = function () {
        return n.fancybox.pos(c + 1)
    }, n.fancybox.prev = function () {
        return n.fancybox.pos(c - 1)
    }, n.fancybox.pos = function (n) {
        if (!u) {
            n = parseInt(n), l = s, n > -1 && n < s.length ? (a = n, ut()) : t.cyclic && s.length > 1 && (a = n >= s.length ? 0 : s.length - 1, ut());
            return
        }
    }, n.fancybox.cancel = function () {
        if (!u) {
            u = !0, n.event.trigger("fancybox-cancel"), yt();
            i.onCancel(l, a, i);
            u = !1
        }
    }, n.fancybox.close = function () {
        function l() {
            w.fadeOut("fast"), o.empty().hide(), r.hide(), n.event.trigger("fancybox-cleanup"), e.empty();
            t.onClosed(s, c, t);
            s = i = [], c = a = 0, t = i = {}, u = !1
        }
        if (!u && !r.is(":hidden")) {
            if (u = !0, t && !1 === t.onCleanup(s, c, t)) {
                u = !1;
                return
            }
            if (yt(), n(it.add(nt).add(g)).hide(), n(e.add(w)).unbind(), n(window).unbind("resize.fb scroll.fb"), n(document).unbind("keydown.fb"), e.find("iframe").attr("src", lt && /^https/i.test(window.location.href || "") ? "javascript:void(false)" : "about:blank"), t.titlePosition !== "inside" && o.empty(), r.stop(), t.transitionOut == "elastic") {
                v = wt();
                var h = r.position();
                f = {
                    top: h.top,
                    left: h.left,
                    width: r.width(),
                    height: r.height()
                }, t.opacity && (f.opacity = 1), o.empty().hide(), d.prop = 1, n(d).animate({
                    prop: 0
                }, {
                    duration: t.speedOut,
                    easing: t.easingOut,
                    step: ct,
                    complete: l
                })
            } else r.fadeOut(t.transitionOut == "none" ? 0 : t.speedOut, l)
        }
    }, n.fancybox.resize = function () {
        w.is(":visible") && w.css("height", n(document).height()), n.fancybox.center(!0)
    }, n.fancybox.center = function () {
        var n, i;
        u || (i = arguments[0] === !0 ? 1 : 0, n = et(), !i && (r.width() > n[0] || r.height() > n[1])) || r.stop().animate({
            top: parseInt(Math.max(n[3] - 20, n[3] + (n[1] - e.height() - 40) * .5 - t.padding)),
            left: parseInt(Math.max(n[2] - 20, n[2] + (n[0] - e.width() - 40) * .5 - t.padding))
        }, typeof arguments[0] == "number" ? arguments[0] : 200)
    }, n.fancybox.init = function () {
        n("#fancybox-wrap").length || (n("body").append(h = n('<div id="fancybox-tmp"></div>'), y = n('<div id="fancybox-loading"><div></div></div>'), w = n('<div id="fancybox-overlay"></div>'), r = n('<div id="fancybox-wrap"></div>')), rt = n('<div id="fancybox-outer"></div>').append('<div class="fancybox-bg" id="fancybox-bg-n"></div><div class="fancybox-bg" id="fancybox-bg-ne"></div><div class="fancybox-bg" id="fancybox-bg-e"></div><div class="fancybox-bg" id="fancybox-bg-se"></div><div class="fancybox-bg" id="fancybox-bg-s"></div><div class="fancybox-bg" id="fancybox-bg-sw"></div><div class="fancybox-bg" id="fancybox-bg-w"></div><div class="fancybox-bg" id="fancybox-bg-nw"></div>').appendTo(r), rt.append(e = n('<div id="fancybox-content"></div>'), it = n('<a id="fancybox-close"></a>'), o = n('<div id="fancybox-title"></div>'), nt = n('<a href="javascript:;" id="fancybox-left"><span class="fancy-ico" id="fancybox-left-ico"></span></a>'), g = n('<a href="javascript:;" id="fancybox-right"><span class="fancy-ico" id="fancybox-right-ico"></span></a>')), it.click(n.fancybox.close), y.click(n.fancybox.cancel), nt.click(function (t) {
            t.preventDefault(), n.fancybox.prev()
        }), g.click(function (t) {
            t.preventDefault(), n.fancybox.next()
        }), n.fn.mousewheel && r.bind("mousewheel.fb", function (t, i) {
            u ? t.preventDefault() : (n(t.target).get(0).clientHeight == 0 || n(t.target).get(0).scrollHeight === n(t.target).get(0).clientHeight) && (t.preventDefault(), n.fancybox[i > 0 ? "prev" : "next"]())
        }), n.support.opacity || r.addClass("fancybox-ie"), lt && (y.addClass("fancybox-ie6"), r.addClass("fancybox-ie6"), n('<iframe id="fancybox-hide-sel-frame" src="' + (/^https/i.test(window.location.href || "") ? "javascript:void(false)" : "about:blank") + '" scrolling="no" border="0" frameborder="0" tabindex="-1"></iframe>').prependTo(rt)))
    }, n.fn.fancybox.defaults = {
        padding: 10,
        margin: 40,
        opacity: !1,
        modal: !1,
        cyclic: !1,
        scrolling: "auto",
        width: 560,
        height: 340,
        autoScale: !0,
        autoDimensions: !0,
        centerOnScroll: !1,
        ajax: {},
        swf: {
            wmode: "transparent"
        },
        hideOnOverlayClick: !0,
        hideOnContentClick: !1,
        overlayShow: !0,
        overlayOpacity: .7,
        overlayColor: "#777",
        titleShow: !0,
        titlePosition: "float",
        titleFormat: null,
        titleFromAlt: !1,
        transitionIn: "fade",
        transitionOut: "fade",
        speedIn: 300,
        speedOut: 300,
        changeSpeed: 300,
        changeFade: "fast",
        easingIn: "swing",
        easingOut: "swing",
        showCloseButton: !0,
        showNavArrows: !0,
        enableEscapeButton: !0,
        enableKeyboardNav: !0,
        onStart: function () {},
        onCancel: function () {},
        onComplete: function () {},
        onCleanup: function () {},
        onClosed: function () {},
        onError: function () {}
    }, n(document).ready(function () {
        n.fancybox.init()
    })
}(jQuery)