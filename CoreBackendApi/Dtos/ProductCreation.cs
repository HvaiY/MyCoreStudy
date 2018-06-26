using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreBackendApi.Dtos
{
    //该模型作为创建的请求参数
    //Validation 验证问题
    public class ProductCreation
    {
        [Display(Name = "产品名称")] //友好名称
        [Required(ErrorMessage = "{0}是必填项")]
        [StringLength(10, MinimumLength = 2, ErrorMessage = "{0}的长度应该不小于{2}, 不大于{1}")] //属性长度限制 最大不超过10 不小于2 {0} 表示属性名称 {1}表示第一个参数  这里是10  {2}表示第二个参数这里是2
        public string Name { get; set; }
        [Display(Name = "价格")]
        [Range(0,Double.MaxValue,ErrorMessage = "{0}的长度应该不小于{2}, 不大于{1}")]
        public float Price { get; set; }
        [Display(Name = "描述")]
        [MaxLength(100, ErrorMessage = "{0}的长度不可以超过{1}")]
        public string Description { get; set; }
    }
}
